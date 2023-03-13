using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    public class Tour : ISerializable, IDable
    {
        private int ID;
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }// string
        public int MaxGuests { get; set; }
        public Stops Stops { get; set; }
        public DateTime StartTour { get; set; } // DateTime startTime
        public int Time { get; set; }
        public List<string> ImagesURL { get; set; }

        public Tour () { }
        public Tour (string name, Location location, string description, String language, int maxGuests, Stops stops, DateTime startTour, int time, List<string> imagesURL)
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

        void ISerializable.FromCSV(string[] values)
        {
            Name = values[0];
            Location = new Location(values[1], values[2]);
            Language = (Language)Enum.Parse(typeof(Language), values[3]);
            MaxGuests = Convert.ToInt32 (values[4]);
            Stops = new Stops(values[5], values[6]);
            StartTour = DateTime.Parse(values[7]);
            Time = Convert.ToInt32 (values[8]);
        }

        string[] ISerializable.ToCSV()
        {
            string[] csvValues = { Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(), Stops.ToString(), Time.ToString() };
            return csvValues;
        }        
    }   
}
