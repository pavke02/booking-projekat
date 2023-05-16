using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class Address : ISerializable, IDable
    {
        private int _id;
        public string Street { get; set; }
        public Location Location { get; set; }

        public Address(string street, Location location)
        {
            Street = street;
            Location = location;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), Street, Location.Country, Location.City };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            Street = values[1];
            Location = new Location(values[2], values[3]);
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
