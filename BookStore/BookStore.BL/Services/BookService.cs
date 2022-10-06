using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }

        public async Task<bool> Add(Book book)
        {
            var books = await _bookRepository.GetAll();

            if (!books.Contains(book))
            {
                return await _bookRepository.Add(book);
            }

            return false;
        }

        public async Task<bool> Update(Book book)
        {
            return await _bookRepository.Update(book);
        }

        public async Task<bool> Delete(int id)
        {
            return await _bookRepository.Delete(id);
        }

        public async Task<Book?> GetBookByNameAndAuthor(string bookName, int authorId)
        {
            return await _bookRepository.GetBookByNameAndAuthor(bookName, authorId);

        }
    }
}
