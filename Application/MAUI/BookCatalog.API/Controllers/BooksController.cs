using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.API;

[Authorize]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    // making a private readonly field of the BookRepository
    private readonly IBookRepository _bookRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IRepository<Picture> _pictureRepository;
    private readonly IMoreInfoRepository _moreInfoRepository;

    // making a private readonly field of the logger
    private readonly ILogger<BooksController> _logger;
    private readonly IMapper _mapper;


    // creating a constructor for the BooksController with dependency injection parameters for the BookRepository and the logger
    public BooksController(IBookRepository bookRepository, IRepository<Picture> pictureRepository, IMoreInfoRepository moreInfoRepository, IPublisherRepository publisherRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, ILogger<BooksController> logger, IMapper mapper)
    {
        try
        {
            // setting the private readonly fields to the dependency injection parameters with a null check
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument(nameof(bookRepository)));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument(nameof(genreRepository)));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument(nameof(authorRepository)));
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument(nameof(publisherRepository)));
            _pictureRepository = pictureRepository ?? throw new ArgumentNullException(nameof(pictureRepository), LoggingStrings.ErrorNullArgument(nameof(pictureRepository)));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument(nameof(logger)));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));
        }
        catch (Exception)
        {
            if (_logger != null)
            {
                // logging that there was an error creating the BooksController if the logger is not null
                _logger.LogError(LoggingStrings.ErrorGeneralMethod($"creating {nameof(BooksController)}"));
            }
            throw;
        }



    }

    // GET: api/Books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
    {
        // returning all the books from the book repository
        try
        {
            // returning all the books from th book repository
            return Ok(_mapper.Map<List<BookDTO>>(await _bookRepository.GetAll()));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting all books (API)"));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // GET: api/Books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDTO>> GetBook(Guid id)
    {
        try
        {
            // getting a book by id
            var book = _mapper.Map<BookDTO>(await _bookRepository.GetById(id));

            // if the book is null, return a 404 not found
            if (book == null)
            {
                return NotFound();
            }

            // return the book with a 200 OK
            return Ok(book);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting book (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // PUT: api/Books/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> PutBook([FromBody] BookCreateDTO BookCreate)
    {
        try
        {
            var Publisher = await _publisherRepository.GetById(BookCreate.PublisherId);

            List<Author> Authors = new();
            List<Genre> Genres = new();
            List<Picture> Pictures = new();
            List<MoreInfo> MoreInfos = new();
            foreach (var AuthorId in BookCreate.AuthorIds)
            {
                Authors.Add(await _authorRepository.GetById(AuthorId));
            }
            foreach (var GenreId in BookCreate.GenreIds)
            {
                Genres.Add(await _genreRepository.GetById(GenreId));
            }
            foreach (var PictureId in BookCreate.PictureIds)
            {
                Pictures.Add(await _pictureRepository.GetById(PictureId));
            }
            foreach (var MoreInfoId in BookCreate.MoreInfoIds)
            {
                MoreInfos.Add(await _moreInfoRepository.GetById(MoreInfoId));
            }

            if (Publisher == null) return NotFound("Publisher not found");

            // Use AutoMapper for flat fields
            var CreatedBook = _mapper.Map<Book>(BookCreate);

            // Attach related entities
            CreatedBook.Publisher = Publisher;
            CreatedBook.Authors = Authors;
            CreatedBook.Genres = Genres;
            CreatedBook.Pictures = Pictures;
            CreatedBook.MoreInfos = MoreInfos;

            // updating the book
            await _bookRepository.Update(CreatedBook);

            // returning no content if the book is updated
            return NoContent();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("updating book (API)", null, BookCreate.Id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // POST: api/Books
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<Book>> PostBook([FromBody] BookCreateDTO BookCreate)
    {
        try
        {
            var Publisher = await _publisherRepository.GetById(BookCreate.PublisherId);

            List<Author> Authors = new();
            List<Genre> Genres = new();
            List<Picture> Pictures = new();
            foreach (var AuthorId in BookCreate.AuthorIds)
            {
                Authors.Add(await _authorRepository.GetById(AuthorId));
            }
            foreach (var GenreId in BookCreate.GenreIds)
            {
                Genres.Add(await _genreRepository.GetById(GenreId));
            }
            foreach (var PictureId in BookCreate.PictureIds)
            {
                Pictures.Add(await _pictureRepository.GetById(PictureId));
            }

            if (Publisher == null) return NotFound("Publisher not found");

            // Use AutoMapper for flat fields
            var CreatedBook = _mapper.Map<Book>(BookCreate);

            // Attach related entities
            CreatedBook.Publisher = Publisher;
            CreatedBook.Authors = Authors;
            CreatedBook.Genres = Genres;
            CreatedBook.Pictures = Pictures;

            // adding a book
            await _bookRepository.Add(CreatedBook);
            return Ok(_mapper.Map<BookDTO>(CreatedBook));
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Posting a book (API)", null));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            // deleting a book
            await _bookRepository.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // logging an error if the book is not found
            _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Book), id));
            return NotFound("Doesn't exist");
        }
        catch (Exception ex)
        {
            // logging an error if there is an exception
            _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting book (API)", null, id));
            return StatusCode(500, "An unexpected error occurred. Please try again later.");
        }

    }
}
