using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class Address : ISerializable
    {
    
        public string Street { get; set; }
        public Location Location { get; set; }

        public Address(string street, Location location)
        {
            Street = street;
            Location = location;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Street, Location.Country, Location.City };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Street = values[0];
            Location = new Location(values[1], values[2]);
        }

        


    }
}
