using SIMS_Booking.Model.Relations;
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
