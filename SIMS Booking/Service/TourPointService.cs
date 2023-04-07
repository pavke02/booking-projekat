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
    public class TourPointService
    {
        private readonly TourPointRepository _repository;

        public TourPointService()
        {
            _repository = new TourPointRepository();
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
