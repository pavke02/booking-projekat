using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;

namespace SIMS_Booking.Service
{
    public class VehicleService
    {
        private readonly VehicleCsvCrudRepository _repository;

        public VehicleService()
        {
            _repository = new VehicleCsvCrudRepository();
        }

        public void Save(Vehicle vehicle)
        {
            _repository.Save(vehicle);
        }

        public List<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public Vehicle GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
