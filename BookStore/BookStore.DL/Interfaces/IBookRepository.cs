using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public  interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAll();

        public Task<Book?> GetById(int id);

        public Task<bool> Add(Book book);

        public Task<bool> Update(Book book);

        public Task<bool> Delete(int id);

        Task<Book?> GetBookByNameAndAuthor(string bookName, int authorId);
    }
}
