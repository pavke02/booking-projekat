using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using System.Linq;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class Accommodation : ISerializable, IDable
    {
        private int _id;
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public User User { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancellationPeriod { get; set; }
        public List<string> ImageURLs { get; set; }
        public bool IsRenovated {get; set; }

        public Accommodation()
        {
            ImageURLs = new List<string>();
        }

        public Accommodation(string name, Location location, AccommodationType type, User user, int maxGuests, int minReservationDays,
            int cancellationPeriod, List<string> imagesURL, bool isRenovated)
        {            
            Name = name;
            Location = location;
            Type = type;
            User = user;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancellationPeriod = cancellationPeriod;
            IsRenovated = isRenovated;
            ImageURLs = new List<string>();
            foreach(string image in imagesURL)
            {
                ImageURLs.Add(image);
            }
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }


        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancellationPeriod = Convert.ToInt32(values[7]);
            ImageURLs = values[8].Split(',').ToList();
            IsRenovated = bool.Parse(values[9]);
        }

        public string[] ToCSV()
        {

            string[] csvValues = { _id.ToString(), Name, Location.Country, Location.City, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancellationPeriod.ToString(), string.Join(',', ImageURLs), IsRenovated.ToString() };
            return csvValues;
        }              
    }
}
