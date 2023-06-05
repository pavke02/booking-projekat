using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class VehicleService
    {
        private readonly ICRUDRepository<Vehicle> _repository;

        public VehicleService(ICRUDRepository<Vehicle> repository)
        {
            _repository = repository;
        }

        #region Crud 

        public void Save(Vehicle vehicle)
        {
            _repository.Save(vehicle);
        }

        public List<Vehicle> GetAll()
        {
            return _repository.GetAll();
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void Delete(Vehicle vehicle)
        {
            _repository.Delete(vehicle);
        }

        #endregion

        public Vehicle GetVehicleByUserID(int id)
        {
            foreach(Vehicle vehicle in _repository.GetAll())
            {
                if(vehicle.UserID == id)
                {
                    return vehicle;
                }
            }
            return null;
        }
    }
}
