using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLocationsService
    {
        private readonly ICRUDRepository<DriverLocations> _repository;

        public DriverLocationsService(ICRUDRepository<DriverLocations> repository)
        {
            _repository = repository;
        }

        public void Save(DriverLocations driverLocations)
        {
            _repository.Save(driverLocations);
        }

        public void AddDriverLocationsToVehicles(VehicleService vehicleService)
        {
            foreach (DriverLocations driverLocations in _repository.GetAll())
            {
                foreach (Vehicle vehicle in vehicleService.GetAll())
                {
                    if (driverLocations.DriverId == vehicle.GetId())
                    {
                        vehicle.Locations.Add(driverLocations.Location);
                    }
                }
            }
        }

        public List<DriverLocations> GetAll()
        {
            return _repository.GetAll();
        }

        public DriverLocations GetDriverLocationsByLocation(string city)
        {
            return _repository.GetAll().FirstOrDefault(d => d.Location.City.ToLower().Contains(city.ToLower()));
        }


    }
}
