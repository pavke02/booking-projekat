using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class DriverLocations : ISerializable, IDable
    {
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
            string[] csvValues = { DriverId.ToString(), Location.Country, Location.City };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            DriverId = int.Parse(values[0]);
            Location = new Location(values[1], values[2]);
        }

        public int GetId()
        {
            return DriverId;
        }

        public void SetId(int id)
        {
            DriverId=id;
        }
    }
}
