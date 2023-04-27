using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLanguagesService
    {
        private readonly RelationsCrudService<DriverLanguages> _crudService;

        public DriverLanguagesService()
        {
            _crudService = new RelationsCrudService<DriverLanguages>("../../../Resources/Data/driverlanguages.csv");
        }

        public void Save(DriverLanguages driverLanguages)
        {
            _crudService.Save(driverLanguages);
        }

        public void AddDriverLanguagesToVehicles(VehicleService vehicleService)
        {
            foreach (DriverLanguages driverLanguages in _crudService.GetAll())
            {
                foreach (Vehicle vehicle in vehicleService.GetAll())
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
