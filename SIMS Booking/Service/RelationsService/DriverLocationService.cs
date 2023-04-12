using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLocationService
    {
        private readonly RelationsCrudService<DriverLocations> _crudService;

        public DriverLocationService()
        {
            _crudService = new RelationsCrudService<DriverLocations>("../../../Resources/Data/driverlocations.csv");
        }

        public DriverLocations GetDriverLocationsByLocation(string city)
        {
            return _crudService.GetAll().FirstOrDefault(d => d.Location.City.ToLower().Contains(city.ToLower()));
        }



    }
}
