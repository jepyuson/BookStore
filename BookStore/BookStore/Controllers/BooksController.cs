using BookStore.DTOs;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookService.GetAllBooks();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBook(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(Book book)
        {
            var message = await _bookService.CreateNewBook(book);
            return Ok(message);
        }

        [HttpPut]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            var message = await _bookService.UpdateBook(book);
            return Ok(message);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var message = await _bookService.DeleteBook(id);
            return Ok(message);
        }
    }
}
