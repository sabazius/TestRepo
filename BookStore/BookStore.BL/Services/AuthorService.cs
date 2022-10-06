using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<IEnumerable<Author>?> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            return await _authorRepository.GetAuthorById(id);
        }

        public async Task AddAuthor(Author author)
        {
            await _authorRepository.AddAuthor(author);
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            return await _authorRepository.UpdateAuthor(author);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.DeleteAuthor(id);
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _authorRepository.GetAuthorByName(name);
        }
    }
}
