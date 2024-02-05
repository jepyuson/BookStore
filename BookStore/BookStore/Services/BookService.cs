using BookStore.Data;
using BookStore.DTOs;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly BookDbContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(BookDbContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                return await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return new List<Book>();
            }
        }
        public async Task<Book> GetBook(int id)
        {
            try
            {
                return await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<string> CreateNewBook(Book book)
        {
            try 
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return $"Created new book: {book.Title}";
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return $"Failed to create new book";
            }       
        }

        public async Task<string> DeleteBook(int id)
        {
            try
            {
                if (id < 1)
                    return "Invalid Id";

                var book = await _context.Books.FindAsync(id);

                if (book == null)
                    return "Book not found";

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return $"Successfully deleted book: {book.Title}";
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return $"Failed to delete book Id: {id}";
            }
        }

        public async Task<string> UpdateBook(Book book)
        {
            try
            {
                if (book == null || book.Id == 0)
                    return "Invalid parameters";

                var bookToUpdate = await _context.Books.FindAsync(book.Id);

                if (bookToUpdate == null)
                    return "Book not found";

                bookToUpdate.CategoryId = book.CategoryId;
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.PublishDateUtc = book.PublishDateUtc;
                await _context.SaveChangesAsync();

                return $"Successfully updated book: {book.Title}";
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return $"Failed to update book: {book.Title}";
            }
        }
    }
}
