using BookCatalog.Services;
using CommunityToolkit.Maui;
using Plugin.LocalNotification;


namespace BookCatalog
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "SolidIcons");
                    fonts.AddFont("fa-regular-400.ttf", "RegularIcons");
                    fonts.AddFont("fa-brands-400.ttf", "BrandLogos");
                })
                .UseMauiMaps();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Register services and view models
            builder.Services.AddSingleton<ICartService, CartService>();
            builder.Services.AddSingleton<IBookCatalogApiService, BookCatalogApiService>();
            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<BooksViewModel>();
            builder.Services.AddSingleton<BookStoresViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddTransient<DetailBookStoreViewModel>();
            builder.Services.AddTransient<BookViewModel>();
            builder.Services.AddSingleton<CartPageViewModel>();
            builder.Services.AddTransient<OrderPlacedViewModel>();
            builder.Services.AddSingleton<ContactViewModel>();

            // Register pages
            builder.Services.AddSingleton<BooksPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<BookPage>();
            builder.Services.AddTransient<OrderPlacedPopup>();
            builder.Services.AddTransient<CartPage>();
            builder.Services.AddSingleton<BookStoresPage>();
            builder.Services.AddSingleton<ContentPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<ContactPage>();

            // Register the popup and its view model
            builder.Services.AddSingleton<ISelfPopupService, SelfPopupService>();

            builder.Services.AddTransientPopup<DetailBookStorePopup, DetailBookStoreViewModel>();

            var app = builder.Build();

            ServiceHelper.Initialize(app.Services);

            return app;
        }
    }
}
