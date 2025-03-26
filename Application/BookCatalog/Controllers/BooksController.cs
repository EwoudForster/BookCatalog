using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using BookCatalog.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ViewResult List()
        {
            ListViewModel listViewModel = new(_bookRepository.GroupBy());
            return View(listViewModel);
        }

        public ViewResult Details(Guid id) 
        {
            DetailsViewModel detailsViewModel = new(_bookRepository.GetById(id));
            return View(detailsViewModel);
        }

        [Authorize]
        public RedirectToActionResult DeleteBook(Guid id)
        {
            var book = _bookRepository.GetById(id);
            _bookRepository.Delete(id);
            return RedirectToAction("List");
        }


    }
}
