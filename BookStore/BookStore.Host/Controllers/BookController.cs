﻿using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger, IAuthorService authorService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;
            _authorService = authorService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            _logger.LogWarning("alabala");
            return Ok(await _bookService.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetId(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null) return NotFound(id);

            return Ok(book);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookRequest bookToUpdate)
        {
            if (bookToUpdate == null) return BadRequest();

            var book = await _bookService.GetById(bookToUpdate.Id);

            if (book == null) return NotFound("Book not exist!");

            var bookForUpdate = _mapper.Map<Book>(bookToUpdate);

            await _bookService.Update(bookForUpdate);

            return Ok(bookToUpdate);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id!");

            var book = await _bookService.GetById(id);

            if (book == null) return NotFound("Book not found!");

            await _bookService.Delete(id);

            return Ok(id);
        }


        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] AddBookRequest addBookRequest)
        {
            if (addBookRequest == null) return BadRequest(addBookRequest);

            var author = await _authorService.GetAuthorByName(addBookRequest.AuthorName);

            if (author == null) return BadRequest("Author not exist!");

            var book = await _bookService.GetBookByNameAndAuthor(addBookRequest.Title, author.Id);

            if (book != null) return BadRequest("Book already exist!");

            var bookToAdd = _mapper.Map<Book>(addBookRequest);

            return Ok(await _bookService.Add(bookToAdd));
        }

        [HttpPost("TestMyClassValidation")]
        public IActionResult TestMyClassValidation([FromBody] ValidatorTestRequest myClass)
        {
            return Ok(myClass);
        }
    }
}
