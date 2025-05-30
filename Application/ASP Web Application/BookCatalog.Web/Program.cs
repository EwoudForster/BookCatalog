using AutoMapper;
using BookCatalog.DAL.Mapping;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Storage.DataBase;
using BookCatalog.DAL.Storage.DataBase.Seeder;
using BookCatalog.DAL.Storage.DataBase.Seeder.Identity;
using BookCatalog.Web;
using BookCatalog.Web.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add logging services
builder.Services.AddLogging();

// Calling on EF Core to use SQL Server using an extension method of the builder variable
// possible way:
// builder.Services.AddDbContext<BookCatalogDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BookCatalog")));
var connectionString = builder.Configuration.GetConnectionString("BookCatalog") ?? throw new InvalidOperationException("Connection string 'BookCatalog' not found.");

builder.Services.AddDbContext<BookDbContext>(options =>
    options
    .UseLazyLoadingProxies().UseSqlServer(connectionString));


var mapperConfig = new MapperConfiguration(cfg =>
{
    // Add your profiles manually here
    cfg.AddProfile<MappingProfile>();
});

// Create the IMapper instance
IMapper mapper = mapperConfig.CreateMapper();

// Register the IMapper instance as a Singleton
builder.Services.AddSingleton(mapper);

builder.Services.AddDefaultIdentity<IdentityUser>()
        .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookDbContext>();


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();

// Enable razor pages
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddOutputCache();

/*builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });
*/

var app = builder.Build();

app.MapDefaultEndpoints();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // Seed roles and users
    await IdentitySeeder.SeedRoles(services);
    await IdentitySeeder.SeedUsers(services, userManager);
}
await DbInitializer.Seed(app);


app.Run();
