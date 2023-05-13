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
                    if (driverLocations.DriverId == vehicle.getID())
                    {
                        vehicle.Locations.Add(driverLocations.Location);
                    }
                }
            }
        }



    }
}
