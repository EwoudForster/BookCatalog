using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers.Api
{
    [Route("api/[controller]")]
    public class BookSearchController : Controller
    {

        private readonly IBookRepository _bookRepository;

        public BookSearchController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpPost]
        public IActionResult Search([FromBody] string query)
        {
            var isValid = Guid.TryParse(query, out _);

            if (isValid)
            {
                var book = _bookRepository.GetById(Guid.Parse(query));
                if (book != null) {
                    return Ok(book);
                   }
            }
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(_bookRepository.GetAll());
            }
            return Ok(_bookRepository.Search(query));
        }
    }
}
