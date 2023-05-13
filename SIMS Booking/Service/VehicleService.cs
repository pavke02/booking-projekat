using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;

namespace SIMS_Booking.Service
{
    public class VehicleService
    {
        private readonly CrudService<Vehicle> _crudService;

        public VehicleService()
        {
            _crudService = new CrudService<Vehicle>(new CsvCrudRepository<Vehicle>());
        }

        #region Crud 

        public void Save(Vehicle vehicle)
        {
            _crudService.Save(vehicle);
        }

        public List<Vehicle> GetAll()
        {
            return _crudService.GetAll();
        }

        public Vehicle GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public Vehicle GetVehicleByUserID(int id)
        {
            foreach(Vehicle vehicle in _crudService.GetAll())
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
