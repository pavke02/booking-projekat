﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;
using SIMS_Booking.View;

namespace SIMS_Booking.Model
{
    public class Tour : ISerializable, IDable
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }// string
        public int MaxGuests { get; set; }
        public List<TourPoint> tourPoints { get; set; }
        public DateTime StartTour { get; set; } // DateTime startTime
        public int Time { get; set; }
        public List<string> ImagesURL { get; set; }
        


        
        public Tour () { tourPoints = new List<TourPoint>(); }
        public Tour (int id, string name, Location location, string description, Language language, int maxGuests, List<TourPoint> stops, DateTime startTour, int time)
        {
            tourPoints = new List<TourPoint>();


            ID = id;
            Name = name;
            Location = location;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            tourPoints = stops;
            StartTour = startTour;
            Time = time;

            
        }



        void ISerializable.FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Language = (Language)Enum.Parse(typeof(Language), values[4]);
            MaxGuests = int.Parse(values[5]);
           // tourPoints = new TourPoint(values[6].Split(',').ToList(),values[7]); 
            StartTour = DateTime.Parse(values[6]);
            Time = Convert.ToInt32 (values[7]);
           // ImagesURL = values[9].Split(',').ToList();
        }

        string[] ISerializable.ToCSV()
        {
           // string[] csvValues = { Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(), string.Join(',',tourPoints), Time.ToString() };
            string[] csvValues = { ID.ToString() ,Name, Location.Country, Location.City, Language.ToString(), MaxGuests.ToString(),  StartTour.ToString(), Time.ToString() };

            return csvValues;
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }
    }

    
}
