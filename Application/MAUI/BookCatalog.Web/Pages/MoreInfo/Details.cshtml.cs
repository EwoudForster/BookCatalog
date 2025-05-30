using AutoMapper;
using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using BookCatalog.Web.Pages.Books;
using BookCatalog.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace BookCatalog.Web.Pages.MoreInfos
{
    [Authorize]
    public class DetailsModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMoreInfoRepository _moreInfoRepository;
        private readonly ILogger<DetailsModel> _logger; 
        private readonly IMapper _mapper;


        public DetailsModel(IBookRepository bookRepository, IMoreInfoRepository MoreInfoRepository, IMapper mapper, ILogger<DetailsModel> logger)
        {
            try
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), LoggingStrings.ErrorNullArgument(nameof(mapper)));

                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _moreInfoRepository = MoreInfoRepository ?? throw new ArgumentNullException(nameof(MoreInfoRepository), LoggingStrings.ErrorNullArgument("MoreInfoRepository"));
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
        public MoreInfoDTO MoreInfo { get; set; }
        [BindProperty]
        public StatisticsViewModel Statistics { get; set; }
        public async Task<IActionResult> OnGet(Guid MoreInfoId)
        {
            try
            {
                ViewData["Title"] = "MoreInfo Details";
                MoreInfo = _mapper.Map<MoreInfoDTO>(await _moreInfoRepository.GetById(MoreInfoId));
                if (MoreInfo == null)
                {
                    return NotFound();
                }
                Statistics = new StatisticsViewModel(await _moreInfoRepository.GetStatisticsForBooksPerMoreInfo(MoreInfoId, BookByStatsOptions.Price),
                    "MoreInfo", "in this MoreInfo");
                return Page();


            }
            catch (InvalidOperationException ex)
            {
                // logging an error if the book is not found
                _logger.LogError(ex, LoggingStrings.ErrorDoesNotExists(nameof(MoreInfo), MoreInfoId));
                return NotFound("Doesn't exist");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, LoggingStrings.ErrorGeneralMethod("getting the page: Details"));
                throw;
            }

        }
    }
}
