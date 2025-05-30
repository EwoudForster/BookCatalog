using BookCatalog.DAL.Repositories.Generic.Async;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Services.Logging;
using BookCatalog.DAL;
using BookCatalog.Web.Pages.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookCatalog.DAL.Models.CalculatedValueModel;
using Microsoft.AspNetCore.Authorization;

namespace BookCatalog.Web.Pages.Publishers
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, ILogger<IndexModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument("publisherRepository"));
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
        public IEnumerable<PublisherCalculated> Publishers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Publishers";
                Publishers = await _publisherRepository.GetAllWithBookStats();
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the homepage"));
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
