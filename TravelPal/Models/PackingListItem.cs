namespace TravelPal.Models
{
    public class PackingListItem
    {
        public string Name { get; set; }
        public int Count { get; set; }

        public PackingListItem(string name, int count)
        {
            Name = name;
            Count = count;
        }
        public PackingListItem(string name)
        {
            Name = name;
            Count = 1;
        }


    }
    public class TravelDocument : PackingListItem
    {
        public bool IsRequired { get; set; }

        public TravelDocument(string name, bool required) : base(name)
        {
            IsRequired = required;
        }



    }
}
