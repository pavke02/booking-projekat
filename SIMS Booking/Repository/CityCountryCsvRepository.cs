using System.Collections.Generic;
using System.IO;

namespace SIMS_Booking.Repository
{
    public class CityCountryCsvRepository 
    {
        private Dictionary<string, List<string>> countries;
        private readonly string path = "../../../Resources/Data/countryCiryDictionary.csv";

        public CityCountryCsvRepository() 
        {
            countries = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, List<string>> Load()
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

                return countries;
            }
        }
    }
}
