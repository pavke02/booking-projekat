using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class DriverLanguagesRepository : RelationsRepository<DriverLanguages>
    {
        public DriverLanguagesRepository() : base("../../../Resources/Data/driverlanguages.csv") { }
        public void AddDriverLanguagesToVehicles(VehicleRepository vehicleRepository)
        {
            foreach (DriverLanguages driverLanguages in _entityList)
            {
                foreach (Vehicle vehicle in vehicleRepository.GetAll())
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
