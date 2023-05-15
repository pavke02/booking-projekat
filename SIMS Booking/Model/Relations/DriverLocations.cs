using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class DriverLocations : ISerializable, IDable
    {
        private int _id;
        public int DriverId { get; set; }
        public Location Location { get; set; }

        public DriverLocations() { }

        public DriverLocations(int driverId, Location location)
        {
            DriverId = driverId;
            Location = location;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), DriverId.ToString(), Location.Country, Location.City };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            DriverId = int.Parse(values[1]);
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
