using SIMS_Booking.Model;
using SIMS_Booking.UI.ViewModel.Driver;
using SIMS_Booking.Utility.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Service
{
    public class RidesService
    {
        private readonly CrudService<Rides> _crudService;

        public RidesService()
        {
            _crudService = new CrudService<Rides>("../../../Resources/Data/rides.csv");
        }

        #region Crud 

        public void Save(Rides ride)
        {
            _crudService.Save(ride);
        }

        public List<Rides> GetAll()
        {
            return _crudService.GetAll();
        }

        public Rides GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Delete(Rides ride)
        {
            _crudService.Delete(ride);
        }

        #endregion

        public List<Rides> GetActiveRides(User user, Vehicle vehicle)
        {
            List<Rides> ActiveRides = new List<Rides>();
            foreach (Rides ride in _crudService.GetAll())
            {
                bool onLocation = false;
                foreach (Location location in vehicle.Locations)
                {
                    if (location.City == ride.Location.City && location.Country == ride.Location.Country)
                    {
                        onLocation = true;
                    }
                }
                if ((ride.DriverID == user.getID() && ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now) || (ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now && ride.Type == "Fast" && onLocation == true))
                {
                    ActiveRides.Add(ride);
                }
            }
            return ActiveRides;
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }
    }
}
