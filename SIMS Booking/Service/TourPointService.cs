using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Service
{
    public class TourPointService
    {
        private readonly CrudService<TourPoint> _crudService;

        public TourPointService()
        {
            _crudService = new CrudService<TourPoint>("../../../Resources/Data/checkpoints.csv");
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public List<TourPoint> GetAll()
        {
            return _crudService.GetAll();
        }

        public void Save(TourPoint tour)
        {
            _crudService.Save(tour);
        }

        public TourPoint GetById(int id)
        {
            return _crudService.GetById(id);
        }

    }
}
