using System;
using System.Collections.Generic;
using System.Text;

namespace Users
{
    public static class UserSession
    {
        public static User? currentUser { get; private set; } = null;

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout()
        {
            currentUser = null;
        }

        public static bool isLoggedIn = currentUser != null;
    }
}
