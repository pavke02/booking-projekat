using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using System.Linq;

namespace SIMS_Booking.Model
{
    public class Accommodation : ISerializable, IDable
    {
        private int ID;
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public User User { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationPeriod { get; set; }
        public List<string> ImageURLs { get; set; }

        public Accommodation() 
        {
            ImageURLs = new List<string>(); 
        }

        public Accommodation(string name, Location location, AccommodationType type, User user, int maxGuests, int minReservationDays, int cancellationPeriod, List<string> imagesURL)
        {            
            Name = name;
            Location = location;
            Type = type;
            User = user;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationPeriod = cancellationPeriod;
            ImageURLs = new List<string>();
            foreach(string image in imagesURL)
            {
                ImageURLs.Add(image);
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
            ID = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancellationPeriod = Convert.ToInt32(values[7]);
            ImageURLs = values[8].Split(',').ToList();
        }

        public string[] ToCSV()
        {

            string[] csvValues = { ID.ToString(), Name, Location.Country, Location.City, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancellationPeriod.ToString(), string.Join(',', ImageURLs)};
            return csvValues;
        }              
    }
}
