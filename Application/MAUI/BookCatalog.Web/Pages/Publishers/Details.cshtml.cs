using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using BookCatalog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BookCatalog.Web.Pages.Publishers
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly ILogger<DetailsModel> _logger;
        private readonly IMapper _mapper;

        public DetailsModel(IBookRepository bookRepository, IPublisherRepository publisherRepository, IMapper mapper, ILogger<DetailsModel> logger)
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
        public PublisherDTO Publisher { get; set; }
        [BindProperty]
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGet(Guid PublisherId)
        {
            try
            {
                ViewData["Title"] = "Publisher Details";
                Publisher = _mapper.Map<PublisherDTO>(await _publisherRepository.GetById(PublisherId));
                if (Publisher == null)
                {
                    return NotFound();
                }
                Statistics = new StatisticsViewModel(await _publisherRepository.GetStatisticsForBooksPerPublisher(PublisherId, BookByStatsOptions.Price),
                   "publisher", "published by this publisher");
                return Page();


            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the book is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(Publisher), PublisherId));
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
