﻿namespace TravelPal.Models
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

        public User(string username, string password, Country location)
        {
            Username = username;
            Password = password;
            Location = location;
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
