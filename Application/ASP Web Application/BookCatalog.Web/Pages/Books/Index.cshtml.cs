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
    public class IndexModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(IBookRepository bookRepository, IGenreRepository genreRepository, ILogger<IndexModel> logger)
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
                    _logger.LogError(LoggingStrings.ErrorCreatingRepository("Book form"));
                }
                throw;
            }
        }

        [BindProperty]
        public IEnumerable<Genre> Genres { get; set; }
        [BindProperty]
        public Guid SelectedGenreId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Books";
                Genres = await _genreRepository.GetAll();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the homepage"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        public async Task<IActionResult> OnPost(Guid? id)
        {
            if(id != null)
            {
                Genres = (IEnumerable<Genre>)await _genreRepository.GetById((Guid)id);
            }
            else
            {
                Genres = await _genreRepository.GetAll();

            }
            return Page();

        }

    }
}
