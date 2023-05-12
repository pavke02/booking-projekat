using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository.RelationsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLocationsService
    {
        private readonly RelationsCrudService<DriverLocations> _crudService;

        public DriverLocationsService()
        {
            _crudService = new RelationsCrudService<DriverLocations>("../../../Resources/Data/driverlocations.csv");
        }

        public void Save(DriverLocations driverLocations)
        {
            _crudService.Save(driverLocations);
        }

        public void AddDriverLocationsToVehicles(VehicleService vehicleService)
        {
            foreach (DriverLocations driverLocations in _crudService.GetAll())
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

        public List<DriverLocations> GetAll()
        {
            return _crudService.GetAll();
        }



    }
}
