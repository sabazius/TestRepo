using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace BookStore.DL.Repositories.SQLRepositories
{
    public class BookSqlRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BookSqlRepository> _logger;
        public BookSqlRepository(IConfiguration configuration, ILogger<BookSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            var result = new List<Book>();
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var books = await conn.QueryAsync<Book>("SELECT * FROM Books WITH (NOLOCK)");

                    return books;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetAll {e.Message}", e);
                }
            }

            return result;
        }

        public async Task<Book?> GetById(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var book = await conn.QueryAsync<Book>("SELECT * FROM Books WHERE ID = @Id", new { Id = id });

                    return book.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                }
            }

            return null;
        }

        public async Task<bool> Add(Book book)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    string query = "INSERT INTO Books (Title, AuthorId, LastUpdated, Quantity, Price) VALUES(@Title, @AuthorId, @LastUpdated, @Quantity, @Price)";

                    var result = await conn.ExecuteAsync(query, book);

                    return result > 0;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.Add {e.Message}", e);
                }
            }

            return false;
        }

        public async Task<bool> Update(Book book)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var query = "UPDATE Books SET Title = @Title, AuthorId = @AuthorId WHERE Id = @Id";

                    var result = await conn.ExecuteAsync(query, book);

                    return result > 0;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.Update {e.Message}", e);
                }
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var query = "DELETE FROM Books WHERE Id = @Id";

                    var result = await conn.ExecuteAsync(query, new { Id = id });

                    return result > 0;
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.Update {e.Message}", e);
                }
            }

            return false;
        }

        public async Task<Book?> GetBookByNameAndAuthor(string bookName, int authorId)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync();

                    var book = await conn.QueryAsync<Book>("SELECT * FROM Books WHERE Title = @Title AND AuthorId = @AuthorId",
                        new { Title = bookName, AuthorId = authorId });

                    return book.FirstOrDefault();
                }
                catch (Exception e)
                {
                    _logger.LogCritical($"Error in BooksRepository.GetById {e.Message}", e);
                }
            }

            return null;
        }
    }
}
