using System.Collections.Generic;

namespace TravelPal.Models
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }


    }

    public class User : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }

        public List<Travel> Travels { get; set; } = new List<Travel>();

        public User(string username, string password, Country location)
        {
            Username = username;
            Password = password;
            Location = location;
        }
        public User(string username, string password, Country location, List<Travel> travels)
        {
            Username = username;
            Password = password;
            Location = location;
            Travels = travels;
        }
    }

    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Country Location { get; set; }

        public Admin(string username, string password, Country location)
        {
            Username = username;
            Password = password;
            Location = location;
        }
    }
}
