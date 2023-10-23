using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Repos
{
    public static class UserManager
    {
        public static List<IUser> Users { get; set; }
        public static IUser SignedInUIser { get; set; }

        public static bool AddUser(IUser user)
        {
            return false;
        }

        public static void RemoveUser(IUser user)
        {

        }
        public static bool UpdateUsername(IUser user, string newUsername)
        {
            return false;
        }
        private static bool ValidateUsername(string username)
        {
            return false;
        }
        public static bool SignInUser(string username, string password)
        {
            return false;
        }
    }
}
