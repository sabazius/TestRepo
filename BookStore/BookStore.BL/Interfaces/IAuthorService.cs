using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;

namespace BookStore.BL.Interfaces
{
    public interface IAuthorService
    {
        public IEnumerable<Author> GetAll();

        public Author? GetAuthorById(int id);

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest);

        public bool UpdateAuthor(Author author);

        public bool DeleteAuthor(int id);

        public Author? GetAuthorByName(string name);

        bool AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
