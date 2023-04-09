using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using System.Linq;

namespace SIMS_Booking.Model
{
    public class Vehicle : ISerializable, IDable
    {
        private int ID;
        public User User { get; set; }
        public int UserID { get; set; }
        public List<Location> Locations { get; set; }
        public int MaxGuests  { get; set; }
        public List<Language> Languages { get; set; }
        public List<string> ImagesURL { get; set; }

        public Vehicle()
        {
            Locations = new List<Location>();
            Languages = new List<Language>();
            ImagesURL = new List<string>();
        }

        public Vehicle(List<Location> locations, int maxGuests, List<Language> languages, List<string> imagesURL, User user)
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
            User = user;
            UserID = user.getID();
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
            ID = Convert.ToInt32(values[0]);
            MaxGuests = Convert.ToInt32(values[1]);
            UserID = Convert.ToInt32(values[2]);
            ImagesURL = values[3].Split(',').ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                ID.ToString(),
                MaxGuests.ToString(),
                UserID.ToString(),
                string.Join(',', ImagesURL)
                };
            return csvValues;
        }
    }
}
