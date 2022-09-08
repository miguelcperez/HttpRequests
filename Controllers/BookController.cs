using HttpRequest.Models;
using HttpRequest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService IBookService)
        {
            _bookService = IBookService;
        }

        [HttpGet, HttpHead]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.None)]
        public async Task<IEnumerable<Book>> Get()
        {
            return (await _bookService.Get());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            Book book = await _bookService.GetById(id);
            if (book == null)
                return NotFound();
            else
                return book;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            await _bookService.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Book book)
        {
            if (!await _bookService.Find(book.Id))
            {
                await _bookService.Add(book);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
            }
            else
            {
                await _bookService.Update(book);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _bookService.Find(id))
            {
                return NotFound();
            }
            else
            {
                await _bookService.Delete(id);
                return NoContent();
            }
        }
    }
}
