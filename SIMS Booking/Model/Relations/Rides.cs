using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Model.Relations
{
    public class Rides : ISerializable
    {
        private int ID { get; set; }

        public int DriverID { get; set; }

        public string Street { get; set; }
        public Location Location { get; set; }

        public DateTime DateTime { get; set; }

        public Rides() { }

        public Rides(int driverID, string street, Location location, DateTime dateTime)
        {
            DriverID = driverID;
            Street = street;
            Location = location;
            DateTime = dateTime;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), DriverID.ToString(), Street, Location.Country, Location.City, DateTime.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            DriverID = Convert.ToInt32(values[1]);
            ID = Convert.ToInt32(values[0]);
            Street = values[2];
            Location = new Location(values[3], values[4]);
            DateTime = Convert.ToDateTime(values[5]);
        }
    }
}
