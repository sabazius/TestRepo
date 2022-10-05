using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using System.Net;

namespace BookStore.BL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorRepository.GetAll();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author != null)
            {

            }

            return await _authorRepository.GetAuthorById(id);
        }

        public async Task<AddAuthorResponse> AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                var auth = await _authorRepository.GetAuthorByName(authorRequest.Name);

                if (auth != null)
                {
                    //log Author already exist
                    return new AddAuthorResponse()
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Message = "Author already exist"
                    };
                }

                var author = _mapper.Map<Author>(authorRequest);
                var result = await _authorRepository.AddAuthor(author);


                return new AddAuthorResponse()
                {
                    HttpStatusCode = HttpStatusCode.OK,
                    Author = result
                };
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAuthor(Author author)
        {
            return await _authorRepository.UpdateAuthor(author);
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.DeleteAuthor(id);
        }

        public async Task<Author?> GetAuthorByName(string name)
        {
            return await _authorRepository.GetAuthorByName(name);
        }

        public async Task<bool> AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return await _authorRepository.AddMultipleAuthors(authorCollection);
        }
    }
}
