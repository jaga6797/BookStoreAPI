using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;

namespace BookStoreAPI.Helpers
{
    public class ApplicationMapper : Profile

    {
        public ApplicationMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();// we are mapping date from books to bookmmodel...we can reverse it byusing  REverseMap();
        }
    }
}
