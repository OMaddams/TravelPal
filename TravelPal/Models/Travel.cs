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
        public double TravelDays { get; set; }
        public double UntilTravel { get; set; }
        public IUser OwnedUser { get; set; }

        public Travel(string destination, Country country, int travellers, List<PackingListItem> packingList, DateTime startDate, DateTime endDate)
        {
            Destination = destination;
            Country = country;
            Travellers = travellers;
            PackingList = packingList;
            StartDate = startDate;
            EndDate = endDate;
            TravelDays = CalculateTravelDays();
            UntilTravel = CalculateUntilTravel();
            if (UserManager.SignedInUIser is not Admin)
            {
                OwnedUser = (User)UserManager.SignedInUIser;
            }

        }

        public double CalculateTravelDays()
        {
            TimeSpan travelDays = EndDate - StartDate;
            return Math.Round(travelDays.TotalDays);
        }
        public double CalculateUntilTravel()
        {
            TimeSpan timespan = StartDate - DateTime.Now;
            return Math.Round(timespan.TotalDays, 1);
        }
        public virtual string GetInfo()
        {
            return Country.ToString();
        }

        public override string ToString()
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
