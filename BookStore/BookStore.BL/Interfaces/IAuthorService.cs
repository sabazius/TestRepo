using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public Task<IEnumerable<Author>> GetAll();

        public Task<Author?> GetAuthorById(int id);

        public Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest);

        public Task<bool> UpdateAuthor(Author author);

        public Task<bool> DeleteAuthor(int id);

        public Task<Author?> GetAuthorByName(string name);

        Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
