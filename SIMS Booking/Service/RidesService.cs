using SIMS_Booking.Model;
using SIMS_Booking.UI.ViewModel.Driver;
using SIMS_Booking.Utility.Observer;
using System;
using System.Collections.Generic;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class RidesService
    {
        private readonly ICRUDRepository<Rides> _repository;

        public RidesService(ICRUDRepository<Rides> repository)
        {
            _repository = repository;
        }

        #region Crud 

        public void Save(Rides ride)
        {
            _repository.Save(ride);
        }

        public List<Rides> GetAll()
        {
            return _repository.GetAll();
        }

        public Rides GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(Rides ride)
        {
            _repository.Delete(ride);
        }

        #endregion

        public List<Rides> GetActiveRides(User user, Vehicle vehicle)
        {
            List<Rides> ActiveRides = new List<Rides>();
            foreach (Rides ride in _repository.GetAll())
            {
                bool onLocation = false;
                foreach (Location location in vehicle.Locations)
                {
                    if (location.City == ride.Location.City && location.Country == ride.Location.Country)
                    {
                        onLocation = true;
                    }
                }
                if ((ride.DriverID == user.GetId() && ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now) || (ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now && ride.Type == "Fast" && onLocation == true))
                {
                    ActiveRides.Add(ride);
                }
            }
            return ActiveRides;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
