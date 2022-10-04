using AutoMapper;
using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using BookStore.Models.Responses;
using System.Collections;
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
        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public Author? GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author != null)
            {

            }

            return _authorRepository.GetAuthorById(id);
        }

        public AddAuthorResponse AddAuthor(AddAuthorRequest authorRequest)
        {
            try
            {
                var auth = _authorRepository.GetAuthorByName(authorRequest.Name);

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
                var result = _authorRepository.AddAuthor(author);


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

        public bool UpdateAuthor(Author author)
        {
            return _authorRepository.UpdateAuthor(author);
        }

        public bool DeleteAuthor(int id)
        {
            return _authorRepository.DeleteAuthor(id);
        }

        public Author? GetAuthorByName(string name)
        {
            return _authorRepository.GetAuthorByName(name);
        }

        public bool AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            return _authorRepository.AddMultipleAuthors(authorCollection);
        }
    }
}
