using BookCatalog.DataLayer.Repositories;
using BookCatalog.Views.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public HomeController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var statisticOptions = BookByStatsOptions.Page;
            var Books = _bookRepository.OrderBookBy(BookByOptions.Year);
            var statistics = _bookRepository.GetBookStatistics();
            var homeViewModel = new HomeViewModel(Books, statistics.average, statistics.max, statistics.min, statistics.totalBooks, statisticOptions);
            return View(homeViewModel);
        }
    }
}
