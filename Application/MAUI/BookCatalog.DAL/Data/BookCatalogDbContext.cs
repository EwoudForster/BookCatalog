using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BookCatalog.DAL.Data;

public class BookCatalogDbContext : IdentityDbContext<User, Roles, Guid>
{
    public BookCatalogDbContext(DbContextOptions<BookCatalogDbContext> options) : base(options)
    {

    }
    public DbSet<LoggingEntry> Logs { get; set; }    
    public DbSet<BookStore> BookStores { get; set; }    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<MoreInfo> MoreInfo { get; set; }

    public DbSet<Book> Books { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Enable lazy loading proxies
        optionsBuilder.UseLazyLoadingProxies();
    }

    // relationship between the tables
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>().HasAlternateKey(b => b.ISBN);

        // one to many relationship between publishers and books
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(p => p.PublisherId);


        modelBuilder.Entity<Book>()
            .HasIndex(b => b.ISBN)
            .IsUnique();

        // many to many relationship between books and authors
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);

        // many to many relationship between books and genres
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books);               
        
        // many to many relationship between books and pictures
        modelBuilder.Entity<Picture>()
            .HasOne(b => b.Book)
            .WithMany(g => g.Pictures)
            .HasForeignKey(a => a.BookId);

        // many to many relationship between books and MoreInfo
        modelBuilder.Entity<Book>()
            .HasMany(b => b.MoreInfos)
            .WithMany(g => g.Books);    
        
        // many to one relationship between Users and Reviews
        modelBuilder.Entity<User>()
            .HasMany(b => b.Reviews)
            .WithOne(g => g.User)
            .HasForeignKey(a => a.UserId);


    }
}
