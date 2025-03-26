using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers.Api
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_bookRepository.GetAll());
        }

        [HttpGet("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            var book = _bookRepository.GetById(Id);
            if (book != null)
            {
                return Ok(book);
            }
            return NotFound($"No Item Found with ID: {Id}");
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid Id)
        {
            if (_bookRepository.GetById(Id) == null)
            {
                return NotFound($"No Item Found with ID: {Id}");
            }
            _bookRepository.Delete(Id);
            return Ok($"Succesfully Deleted Item with ID: {Id}");
        }

        [HttpPut]
        public IActionResult Update(Book book)
        {
            if (_bookRepository.GetById(book.Id) == null)
            {
                return NotFound($"No instance found of object: {book}");
            }
            _bookRepository.Update(book);
            return Ok(book);
        }


        [HttpPost]
        public IActionResult Add(Book book)
        {
            if (_bookRepository.GetById(book.Id) != null)
            {
                return BadRequest($"Object with this id Already Exists: {_bookRepository.GetById(book.Id)}");
            }
            _bookRepository.Add(book);
            return Ok(book);
        }
    }
}
