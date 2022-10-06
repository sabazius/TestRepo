using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>?> GetAll();

        public Task<Author?> GetAuthorById(int id);

        public Task AddAuthor(Author author);

        public Task<bool> UpdateAuthor(Author author);

        public Task<bool> DeleteAuthor(int id);

        public Task<Author?> GetAuthorByName(string name);
    }
}
