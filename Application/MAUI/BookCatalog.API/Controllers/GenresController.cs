using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Logging;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    // making a private readonly field of the GenreRepository
    private readonly IGenreRepository _genreRepository;

    // making a private readonly field of the logger
    private readonly ILogger<GenresController> _logger; 
    private readonly IMapper _mapper; 



    // creating a constructor for the GenresController with dependency injection parameters for the GenreRepository and the logger
    public GenresController(IGenreRepository genreRepository, ILogger<GenresController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument(nameof(genreRepository)));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));


        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the GenresController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(GenresController)}"));
            }
            throw;
        }



    }

    // GET: api/Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        // returning all the genres from the genre repository
        try
        {
            // returning all the genres from the genre repository
            return Ok(_mapper.Map<List<GenreDTO>>(await _genreRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all genres (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Genres/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(Guid id)
    {
        try
        {
            // getting a genre by id
            var genre = _mapper.Map<GenreDTO>(await _genreRepository.GetById(id));

            // if the genre is null, return a 404 not found
            if (genre == null)
            {
                return NotFound();
            }

            // return the genre with a 200 OK
            return Ok(genre);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting genre (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/Genres/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutGenre([FromBody]GenreDTO genre)
    {
        try
        {
            // updating the genre
            await _genreRepository.Update(_mapper.Map<Genre>(genre));

            // returning no content if the genre is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating genre (API)", null, genre.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/Genres
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
   [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Genre>> PostGenre(Genre genre)
    {
        try
        {
            // adding a genre
            await _genreRepository.Add(genre);
            return Ok(genre);
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the genre is not found
            _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Genre), genre.Id));
            return BadRequest("Already exists");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a genre (API)", null, genre.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/Genres/5
    [HttpDelete("{id}")]
   [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        try
        {
            // deleting a genre
            await _genreRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the genre is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Genre), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting genre (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
