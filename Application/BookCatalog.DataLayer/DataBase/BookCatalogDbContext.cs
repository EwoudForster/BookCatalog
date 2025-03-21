using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataLayer.DataBase
{
    public class BookCatalogDbContext : DbContext
    {
        public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}