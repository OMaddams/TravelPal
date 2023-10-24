using System;
using System.Collections.Generic;
using System.Windows;
using TravelPal.Models;

namespace TravelPal.Repos
{
    public static class UserManager
    {
        public static List<IUser> Users { get; set; } = new List<IUser>() { new Admin("admin", "password", Country.Sweden), new User("user", "password", Country.Sweden, new List<Travel> { new Travel("Stockholm", Country.Sweden, 1, new List<PackingListItem> { }, DateTime.Now, DateTime.Now), new Travel("Copenhagen", Country.Denmark, 1, new List<PackingListItem> { }, DateTime.Now, DateTime.Now) }) };
        public static IUser SignedInUIser { get; set; }

        public static bool AddUser(IUser user)
        {
            if (ValidateUsername(user.Username))
            {
                Users.Add(user);
                return true;
            }
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
            foreach (IUser user in Users)
            {
                if (user.Username == username)
                {
                    MessageBox.Show("Username already exists", "Warning");
                    return false;
                }
            }
            return true;
        }
        public static bool SignInUser(string username, string password)
        {
            foreach (IUser user in Users)
            {
                if (user.Username == username)
                {
                    if (user.Password == password)
                    {
                        SignedInUIser = user;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password", "Warning!");
                        return false;
                    }
                }
            }
            MessageBox.Show("No user with that username exists", "Warning!");
            return false;
        }
    }
}
