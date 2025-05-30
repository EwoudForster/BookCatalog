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
public class BookStoresController : ControllerBase
{
    // making a private readonly field of the BookStoresRepository
    private readonly IBookStoreRepository _bookStoreRepository;

    // making a private readonly field of the logger
    private readonly ILogger<BookStoresController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the BookStoresController with dependency injection parameters for the BookStoresRepository and the logger
    public BookStoresController(IBookStoreRepository bookStoreRepository, ILogger<BookStoresController> logger, IMapper mapper)
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

    // GET: api/BookStores
    [HttpGet]
   [Authorize]
    public async Task<ActionResult<IEnumerable<BookStoreDTO>>> GetBookStores()
    {
        // returning all the bookStoress from the bookStores repository
        try
        {
            // returning all the bookStoress from the bookStores repository
            return Ok(_mapper.Map<List<BookStoreDTO>>(await _bookStoreRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all bookStores (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/BookStores/5
    [HttpGet("{id}")]
   [Authorize]
    public async Task<ActionResult<BookStoreDTO>> GetBookStore(Guid id)
    {
        try
        {
            // getting a bookStores by id
            var bookStores = _mapper.Map<BookStoreDTO>(await _bookStoreRepository.GetById(id));

            // if the bookStores is null, return a 404 not found
            if (bookStores == null)
            {
                return NotFound();
            }

            // return the bookStores with a 200 OK
            return Ok(bookStores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting bookStore (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/BookStores/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutBookStore([FromBody] BookStoreDTO bookStore)
    {
        try
        {
            // updating the bookStores
            await _bookStoreRepository.Update(_mapper.Map<BookStore>(bookStore));

            // returning no content if the bookStores is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating bookStore (API)", null, bookStore.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/BookStores
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<BookStoreDTO>> PostBookStore([FromBody] BookStoreDTO bookStore)
    {
        try
        {
            // adding a bookStores
            await _bookStoreRepository.Add(_mapper.Map<BookStore>(bookStore));
            return Ok(bookStore);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the bookStores is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(BookStore), bookStore.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a bookStore (API)", null, bookStore.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/BookStores/5
    [HttpDelete("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBookStore(Guid id)
    {
        try
        {
            // deleting a bookStores
            await _bookStoreRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the bookStores is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(BookStore), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting bookStore (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
