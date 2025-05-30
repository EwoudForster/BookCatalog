using BookCatalog.DAL;
using Microsoft.AspNetCore.Components;

namespace BookCatalog.Web.App.Pages
{
    public partial class BookCard
    {
        [Parameter]
        public Book? Book { get; set; }
    }
}
