using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using System.Data;
using System.Diagnostics;
using System.Printing;

namespace SIMS_Booking.Model
{
    public class Accommodation : ISerializable
    {
        public string Name { get; set; }
        public AccommodationLocation Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuests { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationPeriod { get; set; }
        public List<string> ImagesURL { get; set; }

        public Accommodation() { }

        public Accommodation(string name, AccommodationLocation location, AccommodationType type, int maxGuests, int minReservationDays, int cancelationPeriod, List<string> imagesURL)
        {
            Name = name;
            Location = location;
            Type = type;
            MaxGuests = maxGuests;
            MinReservationDays = minReservationDays;
            CancelationPeriod = cancelationPeriod;
            ImagesURL = imagesURL;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, Location.Country, Location.City, Type.ToString(), MaxGuests.ToString(), MinReservationDays.ToString(), CancelationPeriod.ToString()};
            for (int i = 0; i < ImagesURL.Count; i++)
            {
                    csvValues[i+8] = ImagesURL[i];
            }
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Name = values[0];
            Location = new AccommodationLocation(values[1], values[2]);
            Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[3]);
            MaxGuests = Convert.ToInt32(values[4]);
            MinReservationDays = Convert.ToInt32(values[5]);
            CancelationPeriod = Convert.ToInt32(values[6]);
            int i = 7;
            ImagesURL = new List<string>();
            while (i != values.Length)
            {
                ImagesURL.Add(values[i]);
                i++;
            }

        }
    }
}
