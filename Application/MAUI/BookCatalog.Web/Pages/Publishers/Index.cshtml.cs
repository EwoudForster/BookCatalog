using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO.CalculatedValueModel;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models.CalculatedValueModel;
using BookCatalog.DAL.Repositories;
using BookCatalog.Web.Pages.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BookCatalog.Web.Pages.Publishers
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, IMapper mapper, ILogger<IndexModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository), LoggingStrings.ErrorNullArgument("publisherRepository"));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

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
        public IEnumerable<PublisherCalculatedDTO> Publishers { get; set; }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                ViewData["Title"] = "Publishers";
                Publishers = _mapper.Map<IEnumerable<PublisherCalculatedDTO>>(await _publisherRepository.GetAllWithBookStats());
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
