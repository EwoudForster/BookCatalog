using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalog.Web.Pages.Books
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IBookRepository bookRepository, IGenreRepository genreRepository, ILogger<DetailsModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    _logger.LogError(LoggingStrings.ErrorCreatingRepository("book details page"));
                }
                throw;
            }
        }


        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(Guid BookId)
        {
            try
            {
                ViewData["Title"] = "Book Details";
                Book = await _bookRepository.GetById(BookId);

                if (Book == null)
                {
                    return NotFound();
                }

                // Log the state of Publisher, Authors, and Genres
                _logger.LogInformation("Book Details - Publisher: {Publisher}, Authors Count: {AuthorsCount}, Genres Count: {GenresCount}",
                    Book.Publisher?.Name ?? "None", Book.Authors?.Count() ?? 0, Book.Genres?.Count() ?? 0);

                return Page();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Book), BookId));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the page: Details"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                var book = await _bookRepository.GetById(Book.Id);
                if (book == null)
                {
                    return NotFound();
                }

                await _bookRepository.Delete(book.Id);
                return RedirectToPage("/Books");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("deleting book: Details"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }
    }
}
