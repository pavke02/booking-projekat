using SIMS_Booking.Model.Relations;
using System.Linq;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service.RelationsService
{
    public class DriverLocationService
    {
        private readonly ICRUDRepository<DriverLocations> _repository;

        public DriverLocationService(ICRUDRepository<DriverLocations> repository)
        {
            _repository = repository;
        }

        public DriverLocations GetDriverLocationsByLocation(string city)
        {
            return _repository.GetAll().FirstOrDefault(d => d.Location.City.ToLower().Contains(city.ToLower()));
        }



    }
}
