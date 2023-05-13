using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class Tour : ISerializable, IDable
    {
        private int ID = -1;
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        //public Language Language { get; set; }// string
        public string Language { get; set; }
        public int MaxGuests { get; set; }
        public List<int> TourPointIds { get; set; }
        public List<TourPoint> TourPoints { get; set; }
        public DateTime StartTour { get; set; } // DateTime startTime
        public int Time { get; set; }
        public List<string> ImageURLs { get; set; }
        public int CurrentTourPoint { get; set; }
        public TimeOnly TourTime { get; set; }
        public DateTime EndTimeTour { get; set; }

        public Tour () { TourPoints = new List<TourPoint>(); }
        public Tour ( string name, Location location, string description, string language, int maxGuests, DateTime startTour, int time, List<string> imagesURL, List<int> tourPointIds, List<TourPoint> tourPoints,TimeOnly tourTime)
        {
            TourPoints = tourPoints;            
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
           // tourPoints = stops;
            StartTour = startTour;
            Time = time;
            ImageURLs = new List<string>();
            foreach (string image in imagesURL)
            {
                ImageURLs.Add(image);
            }

            TourPointIds = tourPointIds;
            CurrentTourPoint = 0;
            TourTime = tourTime;

        }
        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
        {
            ID = id;
        }

        void ISerializable.FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Language = values[4];
            MaxGuests = int.Parse(values[5]);
           // tourPoints = new TourPoint(values[6].Split(',').ToList(),values[7]); 
            StartTour = DateTime.Parse(values[6]);
            Time = Convert.ToInt32 (values[7]);
            ImageURLs = values[8].Split(',').ToList();
            TourPointIds = values[9].Split(',').Select(int.Parse).ToList();
            CurrentTourPoint = int.Parse(values[10]);
            TourTime = TimeOnly.Parse(values[11]);
        }

        string[] ISerializable.ToCSV()
        {
           // string[] csvValues = { Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(), string.Join(',',tourPoints), Time.ToString() };
            string[] csvValues = { ID.ToString() ,Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(),  StartTour.ToString(), Time.ToString(), string.Join(',', ImageURLs), string.Join(',',TourPointIds),CurrentTourPoint.ToString(), TourTime.ToString() };

            return csvValues;
        }             
    }   
}
