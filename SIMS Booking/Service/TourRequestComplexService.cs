using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class TourRequestComplexService
    {
        private readonly ICRUDRepository<TourRequestComplex> _repository;

        public TourRequestComplexService(ICRUDRepository<TourRequestComplex> repository)
        {
            _repository = repository;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public List<TourRequestComplex> GetAll()
        {
            return _repository.GetAll();
        }

        public TourRequestComplex? Update(TourRequestComplex entity)
        {

            return _repository.Update(entity);
        }

        public void Delete(TourRequestComplex entity)
        {
            _repository.Delete(entity);

        }
               
        public TourRequestComplex GetById(int id)
        {
            return _repository.GetById(id);
        }





        public List<TourRequestComplex> GetValidTourRequestForGuide()
        {
            List<TourRequestComplex> ComplexTours = new List<TourRequestComplex>();
            List<int> BlackList = new List<int>();
            List<int> bezDuplikata = new List<int>();
            foreach (var tour in  _repository.GetAll())
            {
               if(tour.Requests == Enums.Requests.Accepted)
                {
                    BlackList.Add(tour.GroupId);
                }

                foreach (int broj in BlackList)
                {
                    if (!bezDuplikata.Contains(broj))
                    {
                        bezDuplikata.Add(broj);
                    }
                }                
            }


            foreach (var tour in _repository.GetAll())
            {
                for (int i = 0; i < bezDuplikata.Count; i++)
                {
                    if (tour.GroupId != bezDuplikata[i])
                    {
                        ComplexTours.Add(tour);
                    }
                }

            }                           
            return ComplexTours;

        }


        public List<DateTime> ListAllFreeDates(TourService tourService,TourRequestComplex selectedComplexTour)
        {
            DateTime pocetniDatum = selectedComplexTour.TimeOfStart;
            DateTime trenutniDatum = pocetniDatum;
            List<DateTime> datumi = new List<DateTime>();

            while(trenutniDatum <= selectedComplexTour.TimeOfEnd)
            {
                datumi.Add(trenutniDatum);
                trenutniDatum = trenutniDatum.AddDays(1);
            }
            datumi.RemoveAll(datum => tourService.BusyDates().Contains(datum));
            return datumi;
        }

        
    }
}
