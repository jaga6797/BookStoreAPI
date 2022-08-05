using AutoMapper;
using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookeStoreContext _context;
        private readonly IMapper _mapper;


        public BookRepository(BookeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   
        public async Task<List<BookModel>> GetAllBooksAsync() // to GET all values in table
        {
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).ToListAsync();

            return records;
        }



        public async Task<BookModel> GetBookByIdAsync(int bookId) ///for single value GET
        {
           /* var records = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();

            return records;*/
           //using AUTOMAPPER;
           var book = await _context.Books.FindAsync(bookId);
            return _mapper.Map<BookModel>(book);
        }

        public async Task<int> AddBooksync(BookModel bookModel) //post
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description

            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task UpdateBookAsync(int bookId, BookModel bookModel) ///PUT


        /* {
         var books = await _context.Books.FindAsync(bookId);     //we are hitting await(databae) 2 times..to overcome this we use 2nd method of PUT
         if(books != null)
             {
                 books.Title = bookModel.Title; ;
                 books.Description = bookModel.Description; 
                 await _context.SaveChangesAsync();
             }
        */
        {
            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description

            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();// hitting DB only once here....
        }
        public async Task UpdateBookPatchAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }
         public async Task DeleteBookAsync(int bookId) //delete
           {
               // var book = _context.Books.Where(x => x.Title == "").FirstOrDefault(); //code for fetching data using non primary key 
               var book=new Books() { Id= bookId };

               _context.Books.Remove(book);
               await _context.SaveChangesAsync(); // hitting db only 1 time

           }
        
    }
}

