using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataLayer.DataBase
{
    public class BookCatalogDbContext : IdentityDbContext
    {
        public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}