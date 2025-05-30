using AutoMapper;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BookCatalog.Web.Pages.Books
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IBookRepository bookRepository, IMapper mapper, ILogger<DetailsModel> logger)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [BindProperty]
        public BookDTO Book { get; set; }

        // OnGet loads book details by BookId passed as query param
        public async Task<IActionResult> OnGet(Guid bookId)
        {
            if (bookId == Guid.Empty)
                return BadRequest("Invalid book id.");

            try
            {
                var bookEntity = await _bookRepository.GetById(bookId);
                if (bookEntity == null)
                {
                    return NotFound();
                }

                Book = _mapper.Map<BookDTO>(bookEntity);

                ViewData["Title"] = Book.Title ?? "Book Details";

                _logger.LogInformation("Loaded book details for {Title}", Book.Title);

                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading book details for id {BookId}", bookId);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        public async Task<IActionResult> OnPostDelete()
        {
            if (Book == null || Book.Id == Guid.Empty)
            {
                return BadRequest("Invalid book.");
            }

            try
            {
                var existingBook = await _bookRepository.GetById(Book.Id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                await _bookRepository.Delete(Book.Id);
                _logger.LogInformation("Deleted book {BookId}", Book.Id);

                return RedirectToPage("/Books/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting book {BookId}", Book.Id);
                return StatusCode(500, "An unexpected error occurred while deleting the book.");
            }
        }
    }
}
