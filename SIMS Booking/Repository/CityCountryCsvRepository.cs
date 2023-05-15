using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace SIMS_Booking.Repository
{
    public class CityCountryCsvRepository 
    {
        private Dictionary<string, List<string>> _countries;
        private List<string> _countryNames;
        private ObservableCollection<string> _cityNames;
        private readonly string path = "../../../Resources/Data/countryCityDictionary.csv";

        public CityCountryCsvRepository() 
        {
            _countryNames = new List<string>();
            _cityNames = new ObservableCollection<string>();
            _countries = new Dictionary<string, List<string>>();
        }

        public List<string> LoadCountries()
        {
            using(StreamReader sr = new StreamReader(path))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');                   
                    _countryNames.Add(values[0]);
                }

            }
            return _countryNames;
        }

        public ObservableCollection<string> LoadCitiesForCountry(string country)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                _cityNames.Clear();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');
                    if (values[0] == country)
                        for (int i = 1; i < values.Length; i++)
                            _cityNames.Add(values[i]);
                }

            }
            return _cityNames;
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
                        if (_countries.ContainsKey(key))
                        {
                            _countries[key].Add(values[i]);                            
                        }
                        else
                        {
                            _countries[key] = new List<string>() { values[i] };                            
                        }
                    }
                }

                return _countries;
            }
        }
    }
}
