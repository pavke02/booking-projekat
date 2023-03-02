using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Model
{
    public class ComboBoxLists
    {
        public List<Country> CountriesCollection { get; set; }
        public List<string> TypesCollection { get; set; }

        public ComboBoxLists()
        {
            CountriesCollection = new List<Country>
            {
                new Country("Austria", new List<string>(){"Graz", "Salzburg", "Vienna"}),
                new Country("England", new List<string>(){"Birmingham", "London", "Manchester"}),
                new Country("France", new List<string>(){"Bordeaux", "Marseille", "Paris"}),
                new Country("Germany", new List<string>(){"Berlin", "Frankfurt", "Mainz"}),
                new Country("Italy", new List<string>(){"Milano", "Roma", "Venice"}),
                new Country("Serbia", new List<string>(){"Belgrade", "Novi Sad", "Nis"}),
                new Country("Spain", new List<string>(){"Barcelona", "Madrid", "Malaga"})
            };

            TypesCollection = new List<string>
            {
                "Apartment",
                "House",
                "Cottage"
            };
        }
    }
}
