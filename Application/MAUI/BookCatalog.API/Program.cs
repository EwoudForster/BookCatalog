using AutoMapper;
using BookCatalog.DAL.Data;
using BookCatalog.DAL.Mapping;
using BookCatalog.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BookCatalog.DAL.Storage.DataBase.Seeder;
using System.Threading.Channels;
using BookCatalog.DAL.Models;
using BookCatalog.DAL.Logging;
using BookCatalog.DAL.Seeder.Identity;
using BookCatalog.DAL.FileStorage;


[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    // Add your profiles manually here
    cfg.AddProfile<MappingProfile>();
});

// Create the IMapper instance
IMapper mapper = mapperConfig.CreateMapper();

// Register the IMapper instance as a Singleton
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<BookCatalogDbContext>(options =>
// Use lazy loading proxies, meaning that related data is loaded on demand
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:BookCatalog"]
    )
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Secret"]))
        };
    });



// adding the repository to the services
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGeneralStatisticsRepository, GeneralStatisticsRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IBookStoreRepository, BookStoreRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IRepository<Picture>, GenericRepositoryAsync<Picture>>();
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

// adding controllers
builder.Services.AddControllers();

// swagger structure
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

// enable swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Book Catalog API",
        Description = "An ASP.NET Core Web API for managing books, authors, publishers, and genres."
    });


    // Add JWT Authentication to Swagger UI
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = ParameterLocation.Header
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var requireAuthPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(requireAuthPolicy);

builder.Services.AddIdentityCore<User>()
    .AddRoles<Roles>()
    .AddEntityFrameworkStores<BookCatalogDbContext>();

builder.Services.AddIdentityApiEndpoints<User>();


// Add services to the container.
builder.Services.AddProblemDetails();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Roles>>();

    // Seed roles and users
    await IdentitySeeder.SeedRoles(services);
    await IdentitySeeder.SeedUsers(services, userManager);
}

await DbInitializer.Seed(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<User>();

app.Run();
