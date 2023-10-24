using System;
using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Repos
{
    public static class TravelManager
    {
        public static List<Travel> Travels { get; set; } = new List<Travel>() { new Travel("Stockholm", Country.Sweden, 1, new List<PackingListItem> { }, DateTime.Now, DateTime.Now), new Travel("Copenhagen", Country.Denmark, 1, new List<PackingListItem> { }, DateTime.Now, DateTime.Now) };

        public static void AddTravel(Travel travel)
        {
            Travels.Add(travel);
        }
        public static void RemoveTravel(Travel travel)
        {
            Travels.Remove(travel);
        }
    }
}
