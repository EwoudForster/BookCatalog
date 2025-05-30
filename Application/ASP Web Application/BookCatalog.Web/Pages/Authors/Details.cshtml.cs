using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookCatalog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalog.Web.Pages.Authors
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IBookRepository bookRepository, IAuthorRepository authorRepository, ILogger<DetailsModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository), LoggingStrings.ErrorNullArgument("AuthorRepository"));
            }
            catch (Exception)
            {
                if (_logger != null)
                {
                    _logger.LogError(LoggingStrings.ErrorCreatingRepository("Book form"));
                }
                throw;
            }
        }

        [BindProperty]
        public Author Author { get; set; }
        [BindProperty]
        public StatisticsViewModel Statistics { get; set; }

        public async Task<IActionResult> OnGet(Guid AuthorId)
        {
            try
            {
                ViewData["Title"] = "Author Details";
                Author = await _authorRepository.GetById(AuthorId);

                if (Author == null)
                {
                    return NotFound();
                }
                Statistics = new StatisticsViewModel(await _authorRepository.GetStatisticsForBooksPerAuthor(AuthorId, BookByStatsOptions.Price),
    "author", "printed by this author");
                return Page();

            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the book is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Author), AuthorId));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the page: Details"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
