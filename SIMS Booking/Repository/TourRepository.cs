﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Repository
{
    public class TourRepository : Repository<Tour> , ISubject
    {

        public TourRepository() : base("../../../Resources/Data/guides.csv") { }

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Tour> GetTodaysTours()
        {
            List<Tour> todaysTours = new List<Tour>();
            foreach (Tour tour in _entityList)
            {
                if (DateTime.Today == tour.StartTour)
                {
                    todaysTours.Add(tour);                   
                }
            }
            return todaysTours;
        }

       public void LoadCheckpoints(TourPointRepository tp)
        {
            foreach (var tour in _entityList)
            {
                foreach (var tourPointId in tour.TourPointIds)
                {
                    tour.TourPoints.Add(tp.GetById(tourPointId));//ubacuje objekte tourPoint sa odg ID
                }
            }
        }

    }
}
