using System.Collections.Generic;

namespace TravelPal.Models
{
    public class Travel
    {
        public string Destination { get; set; }
        public Country Country { get; set; }
        public int Travellers { get; set; }
        public List<PackingListItem> MyProperty { get; set; }


    }
}
