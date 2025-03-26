using BookCatalog.DataLayer;
using Microsoft.AspNetCore.Components;

namespace BookCatalog.App.Pages
{
    public partial class BookCard
    {
        [Parameter]
        public Book? Book { get; set; }
    }
}
