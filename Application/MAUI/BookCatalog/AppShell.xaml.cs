using BookCatalog.Services;

namespace BookCatalog;

public partial class AppShell : Shell
{
    public Command FlyoutToggleCommand { get; }

    public Dictionary<string, Type> Routes { get; private set; } = [];
    public AppShell()
    {
        InitializeComponent();

        Routes.Add(nameof(BooksPage), typeof(BooksPage));
        Routes.Add(nameof(BookPage), typeof(BookPage));
        Routes.Add(nameof(HomePage), typeof(HomePage));
        Routes.Add(nameof(ContactPage), typeof(ContactPage));
        Routes.Add(nameof(RegisterPage), typeof(RegisterPage));
        Routes.Add(nameof(LoginPage), typeof(LoginPage));


        foreach (var route in Routes)
        {
            Routing.RegisterRoute(route.Key, route.Value);
        }
        FlyoutToggleCommand = new Command(() => FlyoutIsPresented = !FlyoutIsPresented);
        UpdateTabsBasedOnLogin();

        MessagingCenter.Subscribe<object>(this, "LoginStatusChanged", (sender) =>
        {
            UpdateTabsBasedOnLogin();
        });

    }

    private void UpdateTabsBasedOnLogin()
    {
        Contact.IsVisible = UserSession.IsLoggedIn;
        Books.IsVisible = UserSession.IsLoggedIn;
        Book.IsVisible = UserSession.IsLoggedIn;
        Shop.IsVisible = UserSession.IsLoggedIn;
        Login.IsVisible = !UserSession.IsLoggedIn;
        Register.IsVisible = !UserSession.IsLoggedIn;
        Logout.IsVisible = UserSession.IsLoggedIn;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        UserSession.Logout();
        UpdateTabsBasedOnLogin();
        var tab = Shell.Current.Items.FirstOrDefault(i => i.Route == nameof(LoginPage));
        if (tab != null)
        {
            Shell.Current.CurrentItem = tab;
        }
    }


}