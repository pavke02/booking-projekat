using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    public class Accommodation : ISerializable, IDable
    {
        public int ID { get; set; }
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

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), Name, Location.Country, Location.City, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationPeriod.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Type = (Kind)Enum.Parse(typeof(Kind), values[4]);
            MaxGuests = Convert.ToInt32(values[5]);
            MinReservationDays = Convert.ToInt32(values[6]);
            CancelationPeriod = Convert.ToInt32(values[7]);          
        }        
    }
}
