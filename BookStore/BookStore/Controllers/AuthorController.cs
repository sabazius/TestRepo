using BookStore.BL.Interfaces;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using BookStore.Models.Models;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService,
            ILogger<AuthorController> logger, IMapper mapper)
        {
            _authorService = authorService;
            _logger = logger;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            return Ok(await _authorService.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(nameof(AddAuthorRange))]
        public async Task<IActionResult> AddAuthorRange([FromBody] AddMultipleAuthorsRequest addMultipleAuthorsRequest)
        {
            if (addMultipleAuthorsRequest != null &&
                !addMultipleAuthorsRequest.AuthorRequests.Any())
                return BadRequest(addMultipleAuthorsRequest);

            var authorCollection = _mapper.Map<IEnumerable<Author>>
                (addMultipleAuthorsRequest.AuthorRequests);

            var result = await _authorService.AddMultipleAuthors(authorCollection);

            if (!result) return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorRequest authorRequest)
        {
            //if (authorRequest == null) return BadRequest(authorRequest);

            //var authorExist = _authorService.GetAuthorByName(authorRequest.Name);

            //if (authorExist != null) return BadRequest("Author Already Exist!");

            var result = await _authorService.AddAuthor(authorRequest);

            if (result.HttpStatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(nameof(GetAuthorById))]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            if (id <= 0)
            {
                //log
                return BadRequest($"Parameter id:{id} must be greater than zero!");
            }

            var result = await _authorService.GetAuthorById(id);

            if (result == null)
            {
                //log
                return NotFound(id);
            }

            return Ok(result);
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

    }
}
