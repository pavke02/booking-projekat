using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class TourPointService
    {
        private readonly ICRUDRepository<TourPoint> _repository;

        public TourPointService(ICRUDRepository<TourPoint> repository)
        {
            _repository = repository;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public List<TourPoint> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(TourPoint tour)
        {
            _repository.Save(tour);
        }

        public TourPoint GetById(int id)
        {
            return _repository.GetById(id);
        }

    }
}
