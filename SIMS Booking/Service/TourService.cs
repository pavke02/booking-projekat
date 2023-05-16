using System;
using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class TourService
    {
        private readonly ICRUDRepository<Tour> _repository;

        public TourService(ICRUDRepository<Tour> repository)
        {
            _repository = repository;
        }

        public void Delete(Tour entity)
        {
            _repository.Delete(entity);
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

        public List<Tour> GetFutureTours()
        {
            List<Tour> FutureTours = new List<Tour>();
            foreach (Tour tour in _repository.GetAll())
            {
                if (DateTime.Today < tour.StartTour)
                {
                    FutureTours.Add(tour);
                }
            }
            return FutureTours;
        }

        public List<Tour> GetCompletedTours()
        {
            List<Tour> FutureTours = new List<Tour>();
            foreach (Tour tour in _repository.GetAll())
            {
                if (DateTime.Today > tour.StartTour)
                {
                    FutureTours.Add(tour);
                }
            }
            return FutureTours;
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

        //public List<string> AllPictures(Tour tour)
        //{
        //    List<string> pictures = new List<string>();
        //    foreach (var ture in _crudService.GetAll())
        //    {
        //        foreach(var tourID in tour.getID())
        //        {
        //            pictures.Add(ture.ImageURLs);
        //        }
        //    }
        //}

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

        public bool IsFreeGuideInRangeOfDates(DateTime startTime, DateTime endTime,DateTime exactTime)
        {
           foreach (var tours in _repository.GetAll())
            {
                if (tours.StartTour < startTime && tours.StartTour > endTime && tours.StartTour < exactTime && tours.StartTour > exactTime)
                {
                    return false;
                    
                }
            }
            return true;
        }

        public bool IsFreeGuideInOtherTours(DateTime exactTime)
        {
            foreach (var tours in _repository.GetAll())
            {
                if (tours.StartTour == exactTime)
                {
                    return false;

                }
            }
            return true;
        }
    }
}
