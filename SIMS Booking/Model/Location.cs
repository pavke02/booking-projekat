namespace SIMS_Booking.Model
{
    public class Location
    {
        public string Country { get; set; }
        public string City { get; set; }

        public Location() { }

        public Location(string country, string city)
        {
            Country = country;
            City = city;
        }

        public override string ToString()
        {
            return $"{City}, {Country}";
        }
    }
}
