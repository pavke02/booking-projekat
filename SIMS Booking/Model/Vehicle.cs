using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    public class Vehicle : ISerializable, IDable
    {
        public int ID { get; set; }
        public Location Location { get; set; }
        public int MaxPeople  { get; set; }
        public List<DriverLanguage> Languages { get; set; }
        public List<string> ImagesURL { get; set; }

        public Vehicle() { }

        public Vehicle(Location location, int maxPeople, List<DriverLanguage> languages, List<string> imagesURL)
        {
            Location = location;
            MaxPeople = maxPeople; 
            Languages = new List<DriverLanguage>();
            foreach (DriverLanguage language in languages)
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
            Location = new Location(values[0], values[1]);
            MaxPeople = Convert.ToInt32(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Location.Country, Location.City, MaxPeople.ToString() };
            return csvValues;
        }       
    }
}
