using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Services
{
    public static class UserSession
    {
        public static bool IsLoggedIn { get; private set; }
        public static Guid? UserId { get; private set; }

        public static void SetUser(Guid userId)
        {
            IsLoggedIn = true;
            MessagingCenter.Send(typeof(UserSession), "LoginStatusChanged");
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            MessagingCenter.Send(typeof(UserSession), "LoginStatusChanged");
        }
    }

}
