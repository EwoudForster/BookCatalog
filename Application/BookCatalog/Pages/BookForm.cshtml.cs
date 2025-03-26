using BookCatalog.DataLayer;
using BookCatalog.DataLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookCatalog.Pages
{
    [Authorize]
    public class BookFormModel : PageModel
    {
        private readonly IBookRepository _bookRepository;

        public BookFormModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [BindProperty]
        public bool IsEdit { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                Book = new Book();
                IsEdit = false;
            }
            else
            {
                Book = _bookRepository.GetById(id);
                IsEdit = true;
            }
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ModelState.IsValid)
            {
                if (IsEdit)
                {
                    _bookRepository.Update(Book);
                }
                else
                {
                    _bookRepository.Add(Book);
                }
                return RedirectToAction("Details", "Books",  new { Book.Id });
            }
            return Page();
        }
    }
}
