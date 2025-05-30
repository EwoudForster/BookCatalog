using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL;
using AutoMapper;
using BookCatalog.DAL.DTO;

namespace BookCatalog.ApiService
{
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        // making a private readonly field of the AuthorRepository
        private readonly IAuthorRepository _authorRepository;

        // making a private readonly field of the logger
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMapper _mapper;


        // creating a constructor for the AuthorsController with dependency injection parameters for the AuthorRepository and the logger
        public AuthorsController(IAuthorRepository authorRepository, ILogger<AuthorsController> logger, IMapper mapper)
        {
            try
            {
                // setting the private readonly fields to the dependency injection parameters with a null check
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument(nameof(authorRepository)));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    // logging that there was an error creating the AuthorsController if the logger is not null
                    _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(AuthorsController)}"));
                }
                throw;
            }

        }

        // GET: api/Authors
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            // returning all the authors from the author repository
            try
            {
                // returning all the authors from the author repository
                return Ok(_mapper.Map<List<AuthorDTO>>(await _authorRepository.GetAll(b => b.Books)));
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all authors (API)"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(Guid id)
        {
            try
            {
                // getting a author by id
                var author = _mapper.Map<AuthorDTO>(await _authorRepository.GetById(id, b=> b.Books));

                // if the author is null, return a 404 not found
                if (author == null)
                {
                    return NotFound();
                }

                // return the author with a 200 OK
                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting author (API)", null, id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor([FromBody] AuthorDTO author)
        {
            try
            {
                // updating the author
                await _authorRepository.Update(_mapper.Map<Author>(author));

                // returning no content if the author is updated
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating author (API)", null, author.Id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorDTO>> PostAuthor([FromBody]AuthorDTO author)
        {
            try
            {
                // adding a author
                await _authorRepository.Add(_mapper.Map<Author>(author));
                return Ok(author);
            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the author is not found
                _logger.LogError(ex, LoggingStrings.ErrorAlreadyExists(nameof(Author), author.Id));
                return BadRequest("Already exists");
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a author (API)", null, author.Id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                // deleting a author
                await _authorRepository.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the author is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Author), id));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                // logging an error if there is an exception
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting author (API)", null, id));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }
    }
}
