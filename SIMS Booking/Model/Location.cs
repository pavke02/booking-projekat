using SIMS_Booking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string[] ToCSV()
        {
            string[] csvValues = { Country, City};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Country = values[0];
            City = values[1];
        }

        public override string ToString()
        {
            string location = City + ", " + Country;
            return location;
        }
    }
}
