using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class DriverLocationsCsvCrudRepository : RelationsCsvCrudRepository<DriverLocations>
    {
        public DriverLocationsCsvCrudRepository() : base("../../../Resources/Data/driverlocations.csv") { }
        public void AddDriverLocationsToVehicles(VehicleCsvCrudRepository vehicleCsvCrudRepository)
        {
            foreach (DriverLocations driverLocations in _entityList)
            {
                foreach (Vehicle vehicle in vehicleCsvCrudRepository.GetAll())
                {
                    if (driverLocations.DriverId == vehicle.getID())
                    {
                        vehicle.Locations.Add(driverLocations.Location);
                    }
                }
            }
        }

        public List<DriverLocations> GetAll()
        {
            return _entityList;
        }


    }
}
