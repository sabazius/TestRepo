using System.Diagnostics;
using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.BL.Services;
using BookStore.DL.Interfaces;
using BookStore.Host.AutoMapper;
using BookStore.Host.Controllers;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.Test
{
    public class AuthorTests
    {
        private readonly IMapper _mapper;
        private Mock<ILogger<AuthorController>> _logger;

        private IList<Book> Books = new List<Book>()
        {
            { new Book()
            {
                Id = 1,
                AuthorId = 2,
                Title = "TestTitle"
            } },
            { new  Book()
            {
                Id = 2,
                AuthorId = 3,
                Title = "Another Title"
            }},
        };

        private IList<Author> Authors = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Age = 74,
                DateOfBirth = DateTime.Now,
                Name = "Author Name",
                NickName = "NickName"
            },
            new Author()
            {
                Id = 2,
                Age = 12,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name",
                NickName = "Nick"
            }
        };

        public AuthorTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _logger = new Mock<ILogger<AuthorController>>();
        }

        [Fact]

        public async Task Author_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAll()).Returns(async () => Authors.AsEnumerable());

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.GetAllAuthors();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.Equal(expectedCount, authors.Count());
            Assert.Equal(Authors, authors);
        }

        [Fact]
        public async Task Author_GetAuthorById_IsOk()
        {
            //setup
            var authorId = 1;
            var expectedAuthor = Authors.First(x => x.Id == authorId);

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAuthorById(authorId)).Returns(() => Task.FromResult(Authors.First(x => x.Id == authorId)));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.GetById(authorId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var book = okObjectResult.Value as Author;
            Assert.NotNull(book);
            Assert.Equal(expectedAuthor, book);
        }

        [Fact]
        public async Task Author_GetById_NotFound()
        {
            //setup
            var authorId = 3;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAuthorById(authorId)).Returns(async () => Authors.FirstOrDefault(x => x.Id == authorId));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.GetById(authorId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
            Assert.Equal(authorId,notFoundObjectResult.Value);
        }

        [Fact]
        public async Task AddAuthor_Ok()
        {
            //setup
            var authorRequest = new AddAuthorRequest()
            {
                Name = "Test Author Name",
                Nickname = "MyNickName",
                Age = 22,
                DateOfBirth = DateTime.Now
            };

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                Authors.Add(new Author()
                {
                    Id = 3,
                    Age = authorRequest.Age,
                    Name = authorRequest.Name,
                    DateOfBirth = authorRequest.DateOfBirth,
                    NickName = authorRequest.Nickname
                });
            });

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult.Value);
            Assert.Equal(3, Authors.Count);
        }

        [Fact]
        public async Task AddAuthor_Author_Exist()
        {
            //setup
            var authorRequest = new AddAuthorRequest()
            {
                Age = 12,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name",
                Nickname = "Nick"
            };

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                Authors.Add(new Author()
                {
                    Id = 2,
                    Age = 12,
                    DateOfBirth = DateTime.Now,
                    Name = "Test Author Name",
                    NickName = "Nick"
                });
            });

            mockedAuthorRepository.Setup(x => x.GetAuthorByName(authorRequest.Name)).ReturnsAsync(Authors.FirstOrDefault(x => x.Name.Equals(authorRequest.Name)));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.AddAuthor(authorRequest);

            //Assert
            var badRequestObjectResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestObjectResult.Value);

            var message = badRequestObjectResult.Value as AddUpdateAuthorResponse;
            Assert.Equal($"Author with name: {authorRequest.Name} already exist!", message.Message);
        }

        [Fact]
        public async Task UpdateAuthor_Ok()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Id = 2,
                Age = 12,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name22",
                Nickname = "Nick"
            };

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var author = Authors.FirstOrDefault(x => x.Id == authorRequest.Id);
                Authors.Remove(author);
                Authors.Add(new Author()
                {
                    Id = authorRequest.Id,
                    Name = authorRequest.Name,
                    Age = authorRequest.Age,
                    DateOfBirth = authorRequest.DateOfBirth,
                    NickName = authorRequest.Nickname
                });
            }).ReturnsAsync(true);

            mockedAuthorRepository.Setup(x => x.GetAuthorById(authorRequest.Id))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.UpdateAuthor(authorRequest);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult.Value);
            Assert.NotNull(Authors.FirstOrDefault(x => x.Id == authorRequest.Id && x.Name == authorRequest.Name));
        }

        [Fact]
        public async Task UpdateAuthor_BadRequest()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Id = 2,
                Age = 12,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name22",
                Nickname = "Nick"
            };

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var author = Authors.FirstOrDefault(x => x.Id == authorRequest.Id);
                Authors.Remove(author);
                Authors.Add(new Author()
                {
                    Id = authorRequest.Id,
                    Name = authorRequest.Name,
                    Age = authorRequest.Age,
                    DateOfBirth = authorRequest.DateOfBirth,
                    NickName = authorRequest.Nickname
                });
            }).ReturnsAsync(true);

            mockedAuthorRepository.Setup(x => x.GetAuthorById(authorRequest.Id))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.UpdateAuthor(null);

            var badRequestObjectResult = result as BadRequestResult;
            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestObjectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateAuthor_NotFound()
        {
            //setup
            var authorRequest = new UpdateAuthorRequest()
            {
                Id = 3,
                Age = 12,
                DateOfBirth = DateTime.Now,
                Name = "Test Author Name22",
                Nickname = "Nick"
            };

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var author = Authors.FirstOrDefault(x => x.Id == authorRequest.Id);
                Authors.Remove(author);
                Authors.Add(new Author()
                {
                    Id = authorRequest.Id,
                    Name = authorRequest.Name,
                    Age = authorRequest.Age,
                    DateOfBirth = authorRequest.DateOfBirth,
                    NickName = authorRequest.Nickname
                });
            }).ReturnsAsync(true);

            mockedAuthorRepository.Setup(x => x.GetAuthorById(authorRequest.Id))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == authorRequest.Id));

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.UpdateAuthor(authorRequest);

            var notFoundObjectResult = result as NotFoundObjectResult;
            //Assert
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteAuthor_Ok()
        {
            //setup
            var idToDelete = 1;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAuthorById(idToDelete))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == idToDelete));

            mockedAuthorRepository.Setup(x => x.DeleteAuthor(idToDelete)).Callback(() =>
            {
                Authors.RemoveAt(0);
            }).ReturnsAsync(true);

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.DeleteAuthor(idToDelete);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult.Value);
            Assert.Null(Authors.FirstOrDefault(x => x.Id == idToDelete));
            Assert.Equal(1, Authors.Count);
        }

        [Fact]
        public async Task DeleteAuthor_InvalidId()
        {
            //setup
            var idToDelete = -1;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAuthorById(idToDelete))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == idToDelete));

            mockedAuthorRepository.Setup(x => x.DeleteAuthor(idToDelete)).Callback(() =>
            {
                Authors.RemoveAt(0);
            }).ReturnsAsync(true);

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.DeleteAuthor(idToDelete);

            //Assert
            var badRequestResult = result as BadRequestResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(badRequestResult.StatusCode, (int)HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAuthor_NotFound()
        {
            //setup
            var idToDelete = 11;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();

            mockedAuthorRepository.Setup(x => x.GetAuthorById(idToDelete))
                .Returns(async () => Authors.FirstOrDefault(x => x.Id == idToDelete));

            mockedAuthorRepository.Setup(x => x.DeleteAuthor(idToDelete)).Callback(() =>
            {
                Authors.RemoveAt(0);
            }).ReturnsAsync(true);

            //inject
            var service = new AuthorService(mockedAuthorRepository.Object);
            var controller = new AuthorController(_logger.Object, service, _mapper);

            //Act
            var result = await controller.DeleteAuthor(idToDelete);

            //Assert
            var notFoundResult = result as NotFoundResult;
            Assert.NotNull(notFoundResult);
            Assert.Equal(notFoundResult.StatusCode, (int)HttpStatusCode.NotFound);
        }
    }
}
