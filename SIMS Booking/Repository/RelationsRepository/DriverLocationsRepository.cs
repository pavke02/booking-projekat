using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Observer;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.View;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class DriverLocationsRepository : RelationsRepository<DriverLocations> , ISubject
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

        public void NotifyObservers()
        {
            throw new NotImplementedException();
        }

        public void Subscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(IObserver observer)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<DriverLocations> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
