using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public  interface IAuthorRepository
    {
        public IEnumerable<Author> GetAll();

        public Author? GetAuthorById(int id);

        public Author AddAuthor(Author author);

        public bool UpdateAuthor(Author author);

        public bool DeleteAuthor(int id);

        public Author? GetAuthorByName(string name);

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection);
    }
}
