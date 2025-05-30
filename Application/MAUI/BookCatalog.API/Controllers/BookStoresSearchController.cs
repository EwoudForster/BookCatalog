using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class BookStoresSearchController : ControllerBase
{
    // making a private readonly field of the BookStoresRepository
    private readonly IBookStoreRepository _bookStoreRepository;

    // making a private readonly field of the logger
    private readonly ILogger<BookStoresController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the BookStoresController with dependency injection parameters for the BookStoresRepository and the logger
    public BookStoresSearchController(IBookStoreRepository bookStoreRepository, ILogger<BookStoresController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _bookStoreRepository = bookStoreRepository ?? throw new ArgumentNullException(nameof(bookStoreRepository), LoggingStrings.ErrorNullArgument(nameof(bookStoreRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the BookStoresController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(BookStoresController)}"));
            }
            throw;
        }

    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Search([FromBody] string query)
    {
        try
        {

            // if the query is null or whitespace, return all books
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(_mapper.Map<List<BookStoreDTO>>(await _bookStoreRepository.GetAll()));
            }

            // getting a bookStores by id
            var bookStores = _mapper.Map<List<BookStoreDTO>>(await _bookStoreRepository.Search(query));

            // return the bookStores with a 200 OK
            return Ok(bookStores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting bookStore (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }
}
