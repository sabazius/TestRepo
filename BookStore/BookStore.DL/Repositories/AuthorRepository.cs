using BookStore.DL.InMemoryDb;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {

        public Task<IEnumerable<Author>?> GetAll()
        {
            return Task.FromResult<IEnumerable<Author>>(MockedData.Authors)!;
        }

        public Task<Author?> GetAuthorById(int id)
        {
            return Task.FromResult(MockedData.Authors.SingleOrDefault(x => x.Id == id));
        }

        public Task AddAuthor(Author author)
        {
            MockedData.Authors.Add(author);

            return Task.CompletedTask;
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            var auth = await GetAuthorById(author.Id);

            if (auth == null) return false;

            if (MockedData.Authors.Remove(auth))
            {
                MockedData.Authors.Add(author);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await GetAuthorById(id);

            if (author == null) return false;

            return MockedData.Authors.Remove(author);
        }

        public Task<Author?> GetAuthorByName(string name)
        {
            return Task.FromResult(MockedData.Authors.SingleOrDefault(x => x.Name == name));
        }
    }
}
