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
    public class IndexModel : PageModel
    {

        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<IndexModel> _logger;
        private readonly IMapper _mapper;

        public IndexModel(IBookRepository bookRepository, IOrderRepository orderRepository, IMapper mapper, ILogger<IndexModel> logger)
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
        public PaginatedList<Order> Orders { get; set; }

        [BindProperty]

        public string CurrentFilter { get; set; }
        [BindProperty]

        public string SortOrder { get; set; }
        [BindProperty]

        public DateTime? DateFrom { get; set; }
        [BindProperty]

        public DateTime? DateTo { get; set; }
        [BindProperty]


        public string IdSortParm { get; set; }
        [BindProperty]

        public string CustomerSortParm { get; set; }
        [BindProperty]

        public string DateSortParm { get; set; }
        [BindProperty]

        public string TotalSortParm { get; set; }

        public async Task OnGetAsync(
            string sortOrder,
            string currentFilter,
            string searchString,
            DateTime? dateFrom,
            DateTime? dateTo,
            int? pageIndex)
        {
            Orders = await _orderRepository.GetFilteredOrdersAsync(
                sortOrder,
                currentFilter,
                searchString,
                dateFrom,
                dateTo,
                pageIndex,
                pageSize: 10
            );

            // Optional: set sort param helpers
            IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            CustomerSortParm = sortOrder == "customer" ? "customer_desc" : "customer";
            DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            TotalSortParm = sortOrder == "total" ? "total_desc" : "total";
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            try
            {

                await _orderRepository.Delete(id);

                TempData["SuccessMessage"] = $"Order #{id.ToString().Substring(0, 8)} has been deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting order: {ex.Message}";
            }

            return RedirectToPage("./Index");
        }
    }
}