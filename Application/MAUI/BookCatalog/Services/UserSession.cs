namespace BookCatalog.Services
{
    public static class UserSession
    {
        public static bool IsLoggedIn { get; set; }
        public static string? Email { get; set; }

        public static void SetUser(string email)
        {
            IsLoggedIn = true;
            Email = email;
            MessagingCenter.Send(typeof(UserSession), "LoginStatusChanged");
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            Email = null;
            MessagingCenter.Send(typeof(UserSession), "LoginStatusChanged");
        }
    }

}
