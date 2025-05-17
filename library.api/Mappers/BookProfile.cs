using AutoMapper;
using library.api.Application.Books.Commands;
using library.api.Entities;
using library.api.Models;

namespace library.api.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookCommand, Book>();
            CreateMap<Book, BookModel>();
        }
    }
}
