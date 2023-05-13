using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLanguagesService
    {
        private readonly ICRUDRepository<DriverLanguages> _repository;

        public DriverLanguagesService(ICRUDRepository<DriverLanguages> repository)
        {
            _repository = repository;
        }

        public void Save(DriverLanguages driverLanguages)
        {
            _repository.Save(driverLanguages);
        }

        public void AddDriverLanguagesToVehicles(VehicleService vehicleService)
        {
            foreach (DriverLanguages driverLanguages in _repository.GetAll())
            {
                foreach (Vehicle vehicle in vehicleService.GetAll())
                {
                    if (driverLanguages.DriverId == vehicle.GetId())
                    {
                        vehicle.Languages.Add(driverLanguages.Language);
                    }
                }
            }
        }


    }
}
