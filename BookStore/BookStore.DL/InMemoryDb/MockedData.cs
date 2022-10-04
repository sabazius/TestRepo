using BookStore.Models.Models;

namespace BookStore.DL.InMemoryDb
{
    public static class MockedData
    {
        public static List<Author> Authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Test Name",
                Age = 30,
                DateOfBirth = DateTime.Now,
                NickName = "MyNickname"
            },
            new Author()
            {
                Id = 2,
                Name = "Another Test Name",
                Age = 35,
                DateOfBirth = DateTime.Now,
                NickName = "Another MyNickname"
            }
        };

        public static List<Book> Books = new()
        {
            new Book()
            {
                Id = 1,
                Title = "Vinetu",
                AuthorId = 1
            },
            new Book()
            {
                Id = 2,
                Title = "Shogun",
                AuthorId = 2
            }
        };
    }
}
