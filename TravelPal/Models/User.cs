namespace TravelPal.Models
{
    public interface IUser
    {
        public string username { get; set; }
        public string password { get; set; }
        public Location location { get; set; }

    }
}
