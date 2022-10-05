using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public  interface IAuthorRepository
    {
        public Task<IEnumerable<Author>> GetAll();

        public Task<Author?> GetAuthorById(int id);

        public Task<Author> AddAuthor(Author author);

        public Task<bool> UpdateAuthor(Author author);

        public Task<bool> DeleteAuthor(int id);

        public Task<Author?> GetAuthorByName(string name);

        public Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
