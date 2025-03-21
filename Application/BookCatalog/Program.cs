using BookCatalog.DataLayer;
using BookCatalog.DataLayer.DataBase;
using BookCatalog.DataLayer.DataBase.Seeder;
using BookCatalog.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// adding all the services needed
// making a new instance of the BookService in the container for depencey injection
// AddScoped mean it is the same instance for the whole request
builder.Services.AddScoped<IRepository<Book>, GenericRepository<Book>>();


// adding the DbContext with the connection string as a parameter
builder.Services.AddDbContext<BookCatalogDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["Connectionstrings:BookCatalog"]
    )
);
var app = builder.Build();

// enabling all the services

// Initializing the database
DbInitializer.Seed(app);
app.Run();
