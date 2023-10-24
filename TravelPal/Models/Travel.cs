using System;
using System.Collections.Generic;
using TravelPal.Repos;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public Country Country { get; set; }
        public int Travellers { get; set; }
        public List<PackingListItem> PackingList { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelDays { get; set; }
        public IUser OwnedUser { get; set; }

        public Travel(string destination, Country country, int travellers, List<PackingListItem> packingList, DateTime startDate, DateTime endDate)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
            PackingList = packingList;
            StartDate = startDate;
            EndDate = endDate;
            if (UserManager.SignedInUIser is not Admin)
            {
                OwnedUser = (User)UserManager.SignedInUIser;
            }

        }

        private int CalculateTravelDays()
        {
            return 0;
        }
        public virtual string GetInfo()
        {
            return Country.ToString();
        }

        public string ToString()
        {
            return Destination.ToString() + Country.ToString() + Travellers.ToString() + DateOnly.FromDateTime(StartDate).ToString() + DateOnly.FromDateTime(EndDate).ToString();
        }

    }

    public class WorkTrip : Travel
    {
        public string MeetingDetails { get; set; }

        public WorkTrip(string destination, Country country, int travellers, List<PackingListItem> packingList, DateTime startDate, DateTime endDate, string meetingDetails) : base(destination, country, travellers, packingList, startDate, endDate)
        {
            MeetingDetails = meetingDetails;
        }


        public override string GetInfo()
        {
            return Country.ToString();
        }
    }

    public class Vacation : Travel
    {
        public bool AllInclusive { get; set; }

        public Vacation(string destination, Country country, int travellers, List<PackingListItem> packingList, DateTime startDate, DateTime endDate, bool allInclusive) : base(destination, country, travellers, packingList, startDate, endDate)
        {
            AllInclusive = allInclusive;
        }

        public override string GetInfo()
        {
            return string.Empty;
        }
    }
}
