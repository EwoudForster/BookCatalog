using Microsoft.EntityFrameworkCore;
using BookCatalog.DataLayer.DataBase.Seeder;
using BookCatalog.DataLayer.Repositories;
using BookCatalog.DataLayer.DataBase;
using BookCatalog.App;
using Microsoft.AspNetCore.Identity;

// creating the application
// setup the kerstel server, 
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Calling on EF Core to use SQL Server using an extension method of the builder variable
// possible way:
// builder.Services.AddDbContext<BookCatalogDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BookCatalog")));
builder.Services.AddDbContext<BookCatalogDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BookCatalog"]
    )
);


builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BookCatalogDbContext>();

// initializing MVC
builder.Services.AddControllersWithViews();
// setting up dependency injection
// options
// 1 instance is created for the whole program:
// services.AddSingleton<>();

// every time we ask an instance a new instance is created:
// services.AddTransient<>();

// a new instance is created for every request (default):
// services.AddScoped<>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// Enable razor pages
builder.Services.AddRazorPages(
    options =>
    {
        options.Conventions.AuthorizePage("/BookForm");
    });
//builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Building the application, creates the webapplication
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookCatalogDbContext>();
    dbContext.Database.Migrate();  // Automatically apply pending migrations

    // Seed the database with initial data if needed
    DbInitializer.Seed(app);
}

app.MapDefaultEndpoints();


// this will show the exception page for debugging errors,
// because it may contain secret information
// we surround this best with an if statement to see if we are in development mode, because this is not for production of the application
// we can see this in the properties of our application
// properties > debug > open debug launch profiles ui > environment variables
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// middelware component
// this handles requests for static files, like .css of images
// it looks in the wwwroot file and short circuit the request
// in .NET 9 and ASP.NET 9, a new version of this middleware component was introduced that takes care optimizing delivery:
// mapStaticAssets, does the same thing
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();



// we need another middleware to navigate to our pages, to handle incoming requests
// this initiate some defaults for routing to pages
// this is important to let mvc handle incoming requests on controllers
app.MapDefaultControllerRoute();
app.UseAntiforgery();
//app.MapBlazorHub();

app.MapRazorPages();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// puts the webapplication open for requests
// and really starts an application
app.Run();
