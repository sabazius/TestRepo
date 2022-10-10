using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.Host.AutoMapper;
using BookStore.Host.Controllers;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.Test
{
    public class BookTests
    {
        //private readonly IMapper _mapper;
        //private Mock<IAuthorService> _authorService;
        //private Mock<ILogger<BookController>> _logger;

        //private IList<Book> Books = new List<Book>()
        //{
        //    { new Book()
        //    {
        //        Id = 1,
        //        AuthorId = 2,
        //        Title = "TestTitle"
        //    } },
        //    { new  Book()
        //    {
        //        Id = 2,
        //        AuthorId = 3,
        //        Title = "Another Title"
        //    }},
        //};

        //private IList<Author> Authors = new List<Author>()
        //{
        //    new Author()
        //    {
        //        Id = 2,
        //        Age = 12,
        //        DateOfBirth = DateTime.Now,
        //        Name = "Test Author Name",
        //        NickName = "Nick"
        //    }
        //};

        //public BookTests()
        //{
        //    var mockMapper = new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new AutoMapping());
        //    });

        //    _mapper = mockMapper.CreateMapper();

        //    _authorService = new Mock<IAuthorService>();

        //    _authorService.Setup(x =>  x.GetAuthorByName(Authors.FirstOrDefault().Name))
        //        .ReturnsAsync(Authors.FirstOrDefault());

        //    _logger = new Mock<ILogger<BookController>>();
        //}

        //[Fact]
        //public async Task Book_GetAll_Count_Check()
        //{
        //    //setup
        //    var expectedCount = 2;

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetAll()).Returns(async () => Books.AsEnumerable());

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.GetAllBooks();

        //    //Assert
        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult);

        //    var books = okObjectResult.Value as IEnumerable<Book>;
        //    Assert.NotNull(books);
        //    Assert.Equal(expectedCount, books.Count());
        //    Assert.Equal(Books, books);
        //}

        //[Fact]
        //public async Task Book_GetById_IsOk()
        //{
        //    //setup
        //    var bookId = 1;
        //    var expectedBook = Books.First(x => x.Id == bookId);

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetById(bookId)).Returns(async () => Books.First(x => x.Id == bookId));

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.GetId(bookId);

        //    //Assert
        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult);

        //    var book = okObjectResult.Value as Book;
        //    Assert.NotNull(book);
        //    Assert.Equal(expectedBook, book);
        //}

        //[Fact]
        //public async Task Book_GetById_NotFound()
        //{
        //    //setup
        //    var bookId = 3;

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetById(bookId)).Returns(async () => Books.FirstOrDefault(x => x.Id == bookId));

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.GetId(bookId);

        //    //Assert
        //    var notFoundObjectResult = result as NotFoundObjectResult;
        //    Assert.NotNull(notFoundObjectResult);
        //    Assert.Equal(notFoundObjectResult.Value, bookId);
        //}

        //[Fact]
        //public async Task AddBook_Ok()
        //{
        //    //setup
        //    var bookToAdd = new AddBookRequest()
        //    {
        //        AuthorName = "Test Author Name",
        //        Title = "My title"
        //    };

        //    var mockedBookRepository = new Mock<IBookRepository>();
        //    var book = _mapper.Map<Book>(bookToAdd);


        //    mockedBookRepository.Setup(x => x.Add(It.IsAny<Book>())).Callback(() =>
        //    {
        //        Books.Add(new Book()
        //        {
        //            Id = 23,
        //            AuthorId = 2,
        //            Title = bookToAdd.Title,
        //        });
        //    }).ReturnsAsync(true);

        //    mockedBookRepository.Setup(x => x.GetBookByNameAndAuthor(bookToAdd.Title, 2));

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.AddBook(bookToAdd);

        //    //Assert
        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult.Value);
        //    Assert.Equal(3, Books.Count);
        //}

        //[Fact]
        //public async Task AddBook_Author_Not_Found()
        //{
        //    //setup
        //    var bookToAdd = new AddBookRequest()
        //    {
        //        AuthorName = "Test Author Name No",
        //        Title = "My title"
        //    };

        //    var mockedBookRepository = new Mock<IBookRepository>();
        //    var book = _mapper.Map<Book>(bookToAdd);


        //    mockedBookRepository.Setup(x => x.Add(It.IsAny<Book>())).Callback(() =>
        //    {
        //        Books.Add(book);
        //    }).ReturnsAsync(true);

        //    mockedBookRepository.Setup(x => x.GetBookByNameAndAuthor(bookToAdd.Title, 2)).ReturnsAsync(book);

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.AddBook(bookToAdd);

        //    //Assert
        //    var badRequestObjectResult = result as BadRequestObjectResult;
        //    Assert.NotNull(badRequestObjectResult.Value);
        //    Assert.Equal("Author not exist!", badRequestObjectResult.Value);
        //}

        //[Fact]
        //public async Task AddBook_Book_Exist()
        //{
        //    //setup
        //    var bookToAdd = new AddBookRequest()
        //    {
        //        AuthorName = "Test Author Name",
        //        Title = "TestTitle"
        //    };

        //    var mockedBookRepository = new Mock<IBookRepository>();
        //    var book = _mapper.Map<Book>(bookToAdd);


        //    mockedBookRepository.Setup(x => x.Add(It.IsAny<Book>())).Callback(() =>
        //    {
        //        Books.Add(book);
        //    }).ReturnsAsync(true);

        //    mockedBookRepository.Setup(x => x.GetBookByNameAndAuthor(bookToAdd.Title, 2)).ReturnsAsync(book);

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.AddBook(bookToAdd);

        //    //Assert
        //    var badRequestObjectResult = result as BadRequestObjectResult;
        //    Assert.NotNull(badRequestObjectResult.Value);
        //    Assert.Equal("Book already exist!", badRequestObjectResult.Value);
        //}

        //[Fact]
        //public async Task UpdateBook_Ok()
        //{
        //    //setup
        //    var updateBookRequest = new UpdateBookRequest()
        //    {
        //        Id = 1,
        //        AuthorId = 3,
        //        Title = "TestTitle1"
        //    };

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.Update(It.IsAny<Book>())).Callback(() =>
        //    {
        //        var book = Books.FirstOrDefault(x => x.Id == updateBookRequest.Id);
        //        Books.Remove(book);
        //        Books.Add(new Book()
        //        {
        //            Id = updateBookRequest.Id,
        //            AuthorId = updateBookRequest.AuthorId,
        //            Title = updateBookRequest.Title,
        //        });
        //    }).ReturnsAsync(true);

        //    mockedBookRepository.Setup(x => x.GetById(updateBookRequest.Id))
        //        .Returns(async () => Books.FirstOrDefault(x => x.Id == updateBookRequest.Id));

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.UpdateBook(updateBookRequest);

        //    //Assert
        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult.Value);
        //    Assert.NotNull(Books.FirstOrDefault(x => x.AuthorId == updateBookRequest.AuthorId && x.Title == updateBookRequest.Title));
        //}

        //[Fact]
        //public async Task UpdateBook_NotFound()
        //{
        //    //setup
        //    var updateBookRequest = new UpdateBookRequest()
        //    {
        //        Id = 12,
        //        AuthorId = 3,
        //        Title = "TestTitle1"
        //    };

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.Update(It.IsAny<Book>())).Callback(() =>
        //    {
        //        var book = Books.FirstOrDefault(x => x.Id == updateBookRequest.Id);
        //        Books.Remove(book);
        //        Books.Add(new Book()
        //        {
        //            Id = updateBookRequest.Id,
        //            AuthorId = updateBookRequest.AuthorId,
        //            Title = updateBookRequest.Title,
        //        });
        //    }).ReturnsAsync(true);

        //    mockedBookRepository.Setup(x => x.GetById(updateBookRequest.Id))
        //        .Returns(async () => Books.FirstOrDefault(x => x.Id == updateBookRequest.Id));

        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.UpdateBook(updateBookRequest);

        //    //Assert
        //    var notFoundObjectResult = result as NotFoundObjectResult;
        //    Assert.NotNull(notFoundObjectResult.Value);
        //    Assert.Equal("Book not exist!", notFoundObjectResult.Value);
        //}

        //[Fact]
        //public async Task DeleteBook_Ok()
        //{
        //    //setup
        //    var idToDelete = 1;

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetById(idToDelete))
        //        .Returns(async () => Books.FirstOrDefault(x => x.Id == idToDelete));

        //    mockedBookRepository.Setup(x => x.Delete(idToDelete)).Callback(() =>
        //    {
        //       Books.RemoveAt(0);
        //    }).ReturnsAsync(true);

            
        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.DeleteBook(idToDelete);

        //    //Assert
        //    var okObjectResult = result as OkObjectResult;
        //    Assert.NotNull(okObjectResult.Value);
        //    Assert.Null(Books.FirstOrDefault(x => x.Id == idToDelete));
        //    Assert.Equal(1, Books.Count);
        //}

        //[Fact]
        //public async Task DeleteBook_InvalidId()
        //{
        //    //setup
        //    var idToDelete = -1;

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetById(idToDelete))
        //        .Returns(async () => Books.FirstOrDefault(x => x.Id == idToDelete));

        //    mockedBookRepository.Setup(x => x.Delete(idToDelete)).Callback(() =>
        //    {
        //        Books.RemoveAt(0);
        //    }).ReturnsAsync(true);


        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.DeleteBook(idToDelete);

        //    //Assert
        //    var badRequestObjectResult = result as BadRequestObjectResult;
        //    Assert.NotNull(badRequestObjectResult.Value);
        //    Assert.Equal("Invalid Id!", badRequestObjectResult.Value);
        //}

        //[Fact]
        //public async Task DeleteBook_NotFound()
        //{
        //    //setup
        //    var idToDelete = 11;

        //    var mockedBookRepository = new Mock<IBookRepository>();

        //    mockedBookRepository.Setup(x => x.GetById(idToDelete))
        //        .Returns(async () => Books.FirstOrDefault(x => x.Id == idToDelete));

        //    mockedBookRepository.Setup(x => x.Delete(idToDelete)).Callback(() =>
        //    {
        //        Books.RemoveAt(0);
        //    }).ReturnsAsync(true);


        //    //inject
        //    var service = new BookService(mockedBookRepository.Object);
        //    var controller = new BookController(service, _mapper, _logger.Object, _authorService.Object);

        //    //Act
        //    var result = await controller.DeleteBook(idToDelete);

        //    //Assert
        //    var notFoundObjectResult = result as NotFoundObjectResult;
        //    Assert.NotNull(notFoundObjectResult.Value);
        //    Assert.Equal("Book not found!", notFoundObjectResult.Value);
        //}
    }
}