using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Model
{
    public class Country
    {
        public string Name { get; set; }
        public List<string> Cities { get; set; }

        public Country(string name, List<string> cities)
        {
            Name = name;
            Cities = cities;
        }
    }
}
