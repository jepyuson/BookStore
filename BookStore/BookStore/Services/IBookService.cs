using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBook(int id);
        Task<string> CreateNewBook(Book book);
        Task<string> UpdateBook(Book book);
        Task<string> DeleteBook(int id);
    }
}
