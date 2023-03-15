using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.State;
using System.Linq;

namespace SIMS_Booking.Model
{
    public class Vehicle : ISerializable, IDable
    {
        private int ID;
        public List<Location> Locations { get; set; }
        public int MaxGuests  { get; set; }
        public List<Language> Languages { get; set; }
        public List<string> ImagesURL { get; set; }

        public Vehicle()
        {
            Locations = new List<Location>();
            Languages = new List<Language>();
        }

        public Vehicle(List<Location> locations, int maxGuests, List<Language> languages, List<string> imagesURL)
        {
            Locations = new List<Location>();
            foreach (Location location in locations)
            {
                Locations.Add(location);
            }
            MaxGuests = maxGuests;
            Languages = new List<Language>();
            foreach (Language language in languages)
            {
                Languages.Add(language);
            }
            ImagesURL = new List<string>();
            foreach (string image in imagesURL)
            {
                ImagesURL.Add(image);
            }
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void FromCSV(string[] values)
        {
            // Read ID
            ID = Convert.ToInt32(values[0]);

            // Read Locations
            /*var locations = new List<Location>();
            var locationValues = values[1].Split(';');
            foreach (var locationValue in locationValues)
            {
                var locationParts = locationValue.Split(',');
                var location = new Location
                {
                    Country = locationParts[0],
                    City = locationParts[1]
                };
                locations.Add(location);
            }
            Locations = locations;
            */

            // Read MaxPeople
            MaxGuests = Convert.ToInt32(values[1]);

            // Read DriverLanguages
            /*var driverLanguages = new List<Language>();
            var driverLanguageValues = values[3].Split(',');
            foreach (var driverLanguageValue in driverLanguageValues)
            {
                driverLanguages.Add((Language)Enum.Parse(typeof(Language), driverLanguageValue));
            }
            Languages = driverLanguages;
            */

            // Read ImagesURL
            //ImagesURL = values[3].Split(',').ToList();
        }

        public string[] ToCSV()
        {
            // Convert Locations to string value
            /* var locationValues = Locations.Select(location => $"{location.Country},{location.City}");
            var locationsValue = string.Join(";", locationValues);
            */

            // Convert DriverLanguages to string value
            /*var driverLanguageValues = Languages.Select(driverLanguage => driverLanguage.ToString());
            var driverLanguagesValue = string.Join(",", driverLanguageValues);
            */

            // Create CSV string array
            string[] csvValues = {
                ID.ToString(),
                //locationsValue,
                MaxGuests.ToString(),
                //driverLanguagesValue//,
                //string.Join(',', ImagesURL)
                };
            return csvValues;
        }
    }
}
