using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Service
{
    public class VehicleService
    {
        private readonly VehicleRepository _repository;

        public VehicleService()
        {
            _repository = new VehicleRepository();
        }

        public List<Vehicle> Load()
        {
            return _repository.Load();
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
