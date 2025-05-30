using BookCatalog.DAL;
using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Web.Pages.Authors
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IBookRepository bookRepository, IGenreRepository genreRepository, IAuthorRepository authorRepository, ILogger<IndexModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository), LoggingStrings.ErrorNullArgument("GenreRepository"));
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
        public IEnumerable<AuthorCalculated> Authors { get; set; }
        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Authors";
                Authors = await _authorRepository.GetAllWithBookStats();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("Loading Author Screen"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }

        }
    }
}
