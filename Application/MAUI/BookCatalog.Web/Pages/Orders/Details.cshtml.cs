using AutoMapper;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Web.Pages.Orders
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DetailsModel> _logger;
        private readonly IMapper _mapper;

        public DetailsModel(IBookRepository bookRepository, IOrderRepository orderRepository, IMapper mapper, ILogger<DetailsModel> logger)
        {
            try
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger), LoggingStrings.ErrorNullArgument("Logger"));
                _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository), LoggingStrings.ErrorNullArgument("BookRepository"));
                _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository), LoggingStrings.ErrorNullArgument("orderRepository"));
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
        public Order Order { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Order = await _orderRepository.GetById(id);

            if (Order == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            // Remove all order items first
            _orderRepository.Delete(id);

            TempData["SuccessMessage"] = $"Order #{id.ToString().Substring(0, 8)} has been deleted successfully.";

            return RedirectToPage("./Index");
        }
    }
}