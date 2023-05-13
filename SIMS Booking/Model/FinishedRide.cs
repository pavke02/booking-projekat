using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Model
{
    public class FinishedRide : ISerializable, IDable
    {
        private int ID;
        public Rides Ride { get; set; }
        public string Price { get; set; }
        public string Time { get; set; }

        public FinishedRide() { }

        public FinishedRide(int driverID, Rides ride, string price, string time)
        {
            Ride = ride;
            Price = price;
            Time = time;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), Ride.DriverID.ToString(), Ride.Street, Ride.Location.Country, Ride.Location.City, Ride.DateTime.ToString(), Ride.Fast.ToString(), Price, Time };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            Location rideLocation = new Location();
            rideLocation.Country = values[4];
            rideLocation.City = values[5];
            Ride = new Rides(Convert.ToInt32(values[1]), values[2], rideLocation, Convert.ToDateTime(values[5]), Convert.ToBoolean(values[6]));
            Price = values[7];
            Time = values[8];
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
