﻿using SIMS_Booking.Utility;
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
        public bool Fast { get; set; }
        public Rides() { }

        public Rides(int driverID, string street, Location location, DateTime dateTime, bool fast)
        {
            DriverID = driverID;
            Street = street;
            Location = location;
            DateTime = dateTime;
            Fast = fast;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), DriverID.ToString(), Street, Location.Country, Location.City, DateTime.ToString(), Fast.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            DriverID = Convert.ToInt32(values[1]);
            ID = Convert.ToInt32(values[0]);
            Street = values[2];
            Location = new Location(values[3], values[4]);
            DateTime = Convert.ToDateTime(values[5]);
            Fast = Convert.ToBoolean(values[6]);
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }
    }
}
