using BookCatalog.DAL;
using BookCatalog.DAL.DTO;
using BookCatalog.DAL.Models;
using Microsoft.AspNetCore.Components;

namespace BookCatalog.Web.App.Pages
{
    public partial class BookCard
    {
        [Parameter]
        public BookDTO? Book { get; set; }
    }
}
