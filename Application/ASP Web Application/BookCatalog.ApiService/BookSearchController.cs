using AutoMapper;
using BookCatalog.ApiService;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.ApiService
{
    [Authorize]
    [Route("api/[controller]")]
    public class BookSearchController : ControllerBase
    {

        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookSearchController> _logger;
        private readonly IMapper _mapper;


        public BookSearchController(IBookRepository bookRepository, ILogger<BookSearchController> logger, IMapper mapper)
        {
            try
            {
                // setting the private readonly fields to the dependency injection parameters with a null check
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument(nameof(bookRepository)));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));


            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    // logging that there was an error creating the BooksController if the logger is not null
                    _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(BookSearchController)}"));
                }
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromBody] string query)
        {
            try
            {
                // if the query is null or whitespace, return all books
                if (string.IsNullOrWhiteSpace(query))
                {
                    return Ok(_mapper.Map<BookDTO>(await _bookRepository.GetAll(b => b.Authors, b => b.Publisher, b => b.Genres)));
                }

                // return the search results
                return Ok(await _bookRepository.Search(query));
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("searching books (API)"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
