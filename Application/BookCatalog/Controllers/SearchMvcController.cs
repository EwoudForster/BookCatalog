using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    public class SearchMvcController : Controller
    {

        public IActionResult Index(string query)
        {
            return View();
        }
    }
}
