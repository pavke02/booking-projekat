using System;
using System.Collections.Generic;
using SIMS_Booking.Enums;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    public class Guide : ISerializable, IDable
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public Location Location  { get; set; }
        public string Description { get; set; }
        public DriverLanguages Language { get; set; }
        public int MaxGuests { get; set; }
        public Stops Stops { get; set; }
        public List<DateTime> StartTour { get; set; } 
        public double Time { get; set; }
        public List<string> ImagesURL { get; set; }

        public Guide () { }

        public Guide (string name, Location location, string description, DriverLanguages language, int maxGuests, Stops stops, List<DateTime> startTour, double time, List<string> imagesURL)

        {

            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            Stops = stops;
            StartTour = startTour;
            Time = time;
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
         Name = values[0];
            Location = new Location(values[1], values[2]);
            Language = (DriverLanguages)Enum.Parse(typeof(DriverLanguages), values[3]);

            MaxGuests = Convert.ToInt32 (values[4]);
            Stops = new Stops(values[5], values[6]);
            //starttour = new
            Time = Convert.ToInt32 (values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(), Stops.ToString(), Time.ToString() };
            return csvValues;
        }        
    }
}
