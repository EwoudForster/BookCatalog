using AutoMapper;
using BookCatalog.DAL.Data;
using BookCatalog.DAL.FileStorage;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Mapping;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Repositories;
using BookCatalog.DAL.Seeder.Identity;
using BookCatalog.DAL.Storage.DataBase;
using BookCatalog.DAL.Storage.DataBase.Seeder;
using BookCatalog.Web;
using BookCatalog.Web.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.

// Add logging services
builder.Services.AddLogging();

// Calling on EF Core to use SQL Server using an extension method of the builder variable
// possible way:
// builder.Services.AddDbContext<BookCatalogDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("BookCatalog")));
var connectionString = builder.Configuration.GetConnectionString("BookCatalog") ?? throw new InvalidOperationException("Connection string 'BookCatalog' not found.");

builder.Services.AddDbContext<BookCatalogDbContext>(options =>
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

builder.Services.AddDefaultIdentity<User>()
        .AddRoles<Roles>()
    .AddEntityFrameworkStores<BookCatalogDbContext>();


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IMoreInfoRepository, MoreInfoRepository>();
builder.Services.AddScoped<IRepository<Picture>, GenericRepositoryAsync<Picture>>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IRepository<LoggingEntry>, GenericRepositoryAsync<LoggingEntry>>();

// adding the repository to the services for file logging
builder.Services.AddScoped<ISerialize<LoggingEntry>, JsonFormatter<LoggingEntry>>();
builder.Services.AddScoped<IFileSystem<LoggingEntry>, FileSystem<LoggingEntry>>();
builder.Services.AddScoped<IFileRepository<LoggingEntry>, FileRepository<LoggingEntry>>();

var logChannel = Channel.CreateUnbounded<LoggingEntry>();
builder.Services.AddSingleton(logChannel);
builder.Services.AddHostedService<DbLogQueueService>();
builder.Services.AddHostedService<FileLogQueueService>();

// After registering DbContext, etc.
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddProvider(new DbLoggerProvider(logChannel));
builder.Logging.AddProvider(new FileLoggerProvider(logChannel));

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

app.Run();
