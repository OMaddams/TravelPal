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
        public override string ToString()
        {
            return $"{Name} - Quantity: {Count}";
        }


    }
    public class TravelDocument : PackingListItem
    {
        public bool IsRequired { get; set; }

        public TravelDocument(string name, bool required) : base(name)
        {
            IsRequired = required;
        }

        public override string ToString()
        {
            string required = string.Empty;
            if (IsRequired)
            {
                required = " - Required";
            }

            return $"{Name}{required}";
        }


    }
}
