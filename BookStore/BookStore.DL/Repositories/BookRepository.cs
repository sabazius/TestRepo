using BookStore.DL.InMemoryDb;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;

namespace BookStore.DL.Repositories
{
    public  class BookRepository : IBookRepository
    {
        public Task<IEnumerable<Book>> GetAll()
        {
            return Task.FromResult<IEnumerable<Book>>(MockedData.Books);
        }

        public Task<Book?> GetById(int id)
        {
            return Task.FromResult(MockedData.Books.SingleOrDefault(x => x.Id == id));
        }

        public Task<bool> Add(Book book)
        {
            MockedData.Books.Add(book);

            return Task.FromResult(true);
        }

        public Task<bool> Update(Book book)
        {
            if (MockedData.Books.Remove(book))
            {
                MockedData.Books.Add(book);

                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<bool> Delete(int id)
        {
            var book = await GetById(id);

            if (book == null) return false;

            return MockedData.Books.Remove(book);
        }

        public Task<Book?> GetBookByNameAndAuthor(string bookName, int authorId)
        {
            return Task.FromResult(MockedData.Books.FirstOrDefault(x => string.Equals(x.Title, bookName) && x.AuthorId == authorId));
        }

       
    }
}
