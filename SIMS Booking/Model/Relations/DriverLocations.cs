﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;

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

        public int getID()
        {
            return DriverId;
        }

        public void setID(int id)
        {
            DriverId=id;
        }
    }
}
