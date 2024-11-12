using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using BookShopBusiness;
using BookShopRepository;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

namespace BookShopAPI.Controllers
{
    [Route("odata/[controlller]")]
    public class BooksController : ODataController
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [EnableQuery]
        [HttpGet("GetAll")]
        public IActionResult GetBook()
        {
            var books = _booksRepository.GetAllBooks();
            return Ok(books);
        }

        [EnableQuery]
        [HttpGet("{id}")]
        public IActionResult GetBookById([FromODataUri] int id)
        {
            var book = _booksRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] Books book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _booksRepository.SaveBook(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, [FromBody] Books book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                _booksRepository.UpdateBook(book);
            }
            catch
            {
                if (_booksRepository.GetBookById(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content("Update successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook([FromODataUri] int id)
        {
            var book = _booksRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            _booksRepository.DeleteBook(book);
            return NoContent();
        }
    }
}
