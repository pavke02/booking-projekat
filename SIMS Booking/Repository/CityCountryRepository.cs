using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Repository
{
    public class CityCountryRepository 
    {
        private Dictionary<string, List<string>> countries;
        private readonly string path = "../../../Resources/Data/countryCiryDictionary.csv";

        public CityCountryRepository() 
        {
            countries = new Dictionary<string, List<string>>();
        }

        public void Load()
        {            
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    string key = values[0];
                    for (int i = 1; i < values.Length; i++)
                    {
                        if (countries.ContainsKey(key))
                        {
                            countries[key].Add(values[i]);                            
                        }
                        else
                        {
                            countries[key] = new List<string>() { values[i] };                            
                        }
                    }
                }
            }
        }

        public Dictionary<string, List<string>> GetAll()
        {
            Load();
            return countries;
        }
    }
}
