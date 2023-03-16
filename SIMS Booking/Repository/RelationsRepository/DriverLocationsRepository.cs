using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class DriverLocationsRepository : RelationsRepository<DriverLocations>
    {
        public DriverLocationsRepository() : base("../../../Resources/Data/driverlocations.csv") { }
        public void AddDriverLocationsToVehicles(VehicleRepository vehicleRepository) 
        {
            foreach (DriverLocations driverLocations in _entityList)
            {
                foreach (Vehicle vehicle in vehicleRepository.GetAll())
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
