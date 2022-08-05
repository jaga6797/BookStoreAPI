using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();//getall
        Task<BookModel> GetBookByIdAsync(int bookId);//get single
        Task<int> AddBooksync(BookModel bookModel);//post

        Task UpdateBookAsync(int bookId, BookModel bookModel);//put

        Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel);//patch
        Task DeleteBookAsync(int bookId); //delete
    }

}
