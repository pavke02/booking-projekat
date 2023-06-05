using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;

namespace SIMS_Booking.Model
{
    public class Rides : ISerializable, IDable
    {
        private int ID;
        public int DriverID { get; set; }
        public string Street { get; set; }
        public Location Location { get; set; }
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public bool Pending { get; set; }
        public Rides() { }

        public Rides(int driverID, string street, Location location, DateTime dateTime, string type, bool pending)
        {
            DriverID = driverID;
            Street = street;
            Location = location;
            DateTime = dateTime;
            Type = type;
            Pending = pending;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), DriverID.ToString(), Street, Location.Country, Location.City, DateTime.ToString(), Type, Pending.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            DriverID = Convert.ToInt32(values[1]);
            ID = Convert.ToInt32(values[0]);
            Street = values[2];
            Location = new Location(values[3], values[4]);
            DateTime = Convert.ToDateTime(values[5]);
            Type = values[6];
            Pending = Convert.ToBoolean(values[7]);
        }

        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
        {
            ID = id;
        }
    }
}
