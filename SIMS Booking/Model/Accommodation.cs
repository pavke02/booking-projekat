using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;

namespace SIMS_Booking.Model
{
    public class Accommodation : ISerializable
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public Kind Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationPeriod { get; set; }
        public List<string> ImagesURL { get; set; }

        public Accommodation() { }

        public Accommodation(string name, Location location, Kind type, int maxGuests, int minReservationDays, int cancelationPeriod, List<string> imagesURL)
        {
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationPeriod = cancelationPeriod;
            ImagesURL = new List<string>();
            foreach(string image in imagesURL)
            {
                ImagesURL.Add(image);
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, Location.Country, Location.City, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationPeriod.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Location = new Location(values[1], values[2]);
            Type = (Kind)Enum.Parse(typeof(Kind), values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            CancelationPeriod = Convert.ToInt32(values[6]);          
        }
    }
}
