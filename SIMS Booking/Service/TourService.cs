using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class TourService
    {
        private readonly TourRepository _repository;

        public TourService()
        {
            _repository = new TourRepository();
        }


        public List<Tour> GetTodaysTours()
        {
            List<Tour> todaysTours = new List<Tour>();
            foreach (Tour tour in _repository.GetAll())
            {
                if (DateTime.Today == tour.StartTour)
                {
                    todaysTours.Add(tour);
                }
            }
            return todaysTours;
        }

       
        public void LoadCheckpoints(TourPointService tp)
        {
            foreach (var tour in _repository.GetAll())
            {
                foreach (var tourPointId in tour.TourPointIds)
                {
                    tour.TourPoints.Add(tp.GetById(tourPointId));//ubacuje objekte tourPoint sa odg ID
                }
            }
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }


        public List<Tour> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(Tour tour)
        {
           _repository.Save(tour);
        }
        public bool ValidTimeOfTour()
        {
            foreach (var tour in _repository.GetAll())
            {
                foreach (var tour1 in _repository.GetAll())
                {
                    if (tour.TourTime.AddHours(tour.Time) > tour1.TourTime.AddHours(tour1.Time))
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        public int CountCheckPoints(string text)
        {
            string[] checkpoints = text.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return checkpoints.Length;
        }


    }
}
