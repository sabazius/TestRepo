using System.Net;
using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _authorService = authorService;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authorService.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null) return NotFound(id);

            return Ok(author);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorRequest authorRequest)
        {
            if (authorRequest == null) return BadRequest();

            var author = await _authorService.GetAuthorById(authorRequest.Id);

            if (author == null) return NotFound(authorRequest);

            var authorForUpdate = _mapper.Map<Author>(authorRequest);

            var result = await _authorService.UpdateAuthor(authorForUpdate);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (id <= 0) return BadRequest();

            var author = await _authorService.GetAuthorById(id);

            if (author == null) return NotFound();

            await _authorService.DeleteAuthor(id);

            return Ok(id);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AddAuthorRequest authorRequest)
        {

            if (await _authorService.GetAuthorByName(authorRequest.Name) != null)
            {
                return BadRequest(new AddUpdateAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Message = $"Author with name: {authorRequest.Name} already exist!"
                });
            }

            var author = _mapper.Map<Author>(authorRequest);

            await _authorService.AddAuthor(author);

            return Ok(new AddUpdateAuthorResponse()
            {
                HttpStatusCode = HttpStatusCode.OK,
            });
        }
    }
}