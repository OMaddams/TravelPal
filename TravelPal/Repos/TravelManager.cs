using System;
using System.Collections.Generic;
using TravelPal.Models;

namespace TravelPal.Repos
{
    public static class TravelManager
    {
        public static List<Travel> Travels { get; set; } = new List<Travel>() { new Travel("Stockholm", Country.Sweden, 1, new List<PackingListItem> { new PackingListItem("Socks", 5), new PackingListItem("Laptop") }, new DateTime(2025, 01, 13), new DateTime(2025, 01, 23)), new Travel("Copenhagen", Country.Denmark, 1, new List<PackingListItem> { new PackingListItem("Socks", 5), new PackingListItem("Laptop") }, new DateTime(2024, 6, 5), new DateTime(2024, 7, 5)) };

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
