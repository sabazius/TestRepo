using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.MsSql
{

    public class AuthorRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;

        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Author>("prcGetAllAuthors");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}", e);
            }

            return Enumerable.Empty<Author>();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WHERE Id = @Id", new {Id = id});
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    string query = "INSERT INTO Authors (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)";

                    var result = await conn.ExecuteScalarAsync(query, author);

                    return author;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.Add: {e.Message}", e);
                }
            }

            return null;
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var query = "UPDATE Authors SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, NickName = @NickName WHERE Id = @Id";

                    var result = await conn.ExecuteAsync(query, author);

                    return result > 0;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.Update {e.Message}", e);
                }
            }

            return false;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WHERE Name = @Name", new { Name = name});
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}", e);
            }

            return null;
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result  = await conn.ExecuteAsync("INSERT INTO [Authors] (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)",
                        authorCollection);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAll)}:{e.Message}", e);
            }

            return false;
        }
    }
}
