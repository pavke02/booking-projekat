using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class DriverLanguagesCsvCrudRepository : RelationsCsvCrudRepository<DriverLanguages>
    {
        public DriverLanguagesCsvCrudRepository() : base("../../../Resources/Data/driverlanguages.csv") { }
        public void AddDriverLanguagesToVehicles(VehicleCsvCrudRepository vehicleCsvCrudRepository)
        {
            foreach (DriverLanguages driverLanguages in _entityList)
            {
                foreach (Vehicle vehicle in vehicleCsvCrudRepository.GetAll())
                {
                    if (driverLanguages.DriverId == vehicle.getID())
                    {
                        vehicle.Languages.Add(driverLanguages.Language);
                    }
                }
            }
        }
    }
}
