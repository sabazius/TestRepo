using BookStore.DL.InMemoryDb;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using Microsoft.Extensions.Logging;

namespace BookStore.DL.Repositories.InMemoryRepositories
{
    public class AuthorInMemoryRepository 
    {
        private readonly ILogger<AuthorInMemoryRepository> _logger;

        public AuthorInMemoryRepository(ILogger<AuthorInMemoryRepository> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Author> GetAll()
        {
            return MockedData.Authors;
        }

        public Author? GetAuthorById(int id)
        {
            return MockedData.Authors.SingleOrDefault(x => x.Id == id);
        }

        public Author AddAuthor(Author author)
        {
            MockedData.Authors.Add(author);

            return author;
        }

        public bool UpdateAuthor(Author author)
        {
            var auth = GetAuthorById(author.Id);

            if (auth == null) return false;

            if (MockedData.Authors.Remove(auth))
            {
                MockedData.Authors.Add(author);

                return true;
            }

            return false;
        }

        public bool DeleteAuthor(int id)
        {
            var author = GetAuthorById(id);

            if (author == null) return false;

            return MockedData.Authors.Remove(author);
        }

        public Author? GetAuthorByName(string name)
        {
            return MockedData.Authors.SingleOrDefault(x => x.Name == name);
        }

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                MockedData.Authors.AddRange(authorCollection);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning(
                    $"Unable to add multiple authors with message:{e.Message}");
                return false;
            }
        }
    }
}
