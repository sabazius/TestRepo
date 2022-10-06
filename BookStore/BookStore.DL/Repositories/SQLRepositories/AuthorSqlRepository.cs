using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace BookStore.DL.Repositories.SQLRepositories
{
    public class AuthorSqlRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookSqlRepository> _logger;

        public AuthorSqlRepository(IConfiguration configuration, ILogger<BookSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Author>?> GetAll()
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var authors = await conn.QueryAsync<Author>("SELECT * FROM Authors");

                    return authors;
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Error in AuthorRepository.GetAll: {e.Message}", e);
            }

            return null;
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var author = await conn.QueryAsync<Author>("SELECT * FROM Authors WHERE ID = @Id", new { Id = id });

                    return author.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.GetAuthorById: {e.Message}", e);
                }
            }

            return null;
        }

        public async Task AddAuthor(Author author)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    string query = "INSERT INTO Authors (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)";

                    var result = await conn.ExecuteAsync(query, author);
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.Add: {e.Message}", e);
                }
            }
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
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var query = "DELETE FROM Authors WHERE Id = @Id";

                    var result = await conn.ExecuteAsync(query, new { Id = id });

                    return result > 0;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.Update {e.Message}", e);
                }
            }

            return false;
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var author = await conn.QueryAsync<Author>("SELECT * FROM Authors WHERE Name = @Name",
                        new { Name = name});

                    return author.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in AuthorRepository.GetAuthorByName {e.Message}", e);
                }
            }

            return null;
        }
    }
}
