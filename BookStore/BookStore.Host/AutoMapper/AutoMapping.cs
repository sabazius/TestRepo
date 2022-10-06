using AutoMapper;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.Host.AutoMapper
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<AddBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>();
            CreateMap<UpdateAuthorRequest, Author>();
        }
    }
}
