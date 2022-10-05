﻿using AutoMapper;
using BookStore.Models.Models;
using BookStore.Models.Requests;

namespace BookStore.AutoMapper
{
    internal class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();

        }
    }
}
