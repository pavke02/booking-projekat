using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class ConfirmTourService
    {


        private readonly ConfirmTourRepository _repository;

        public ConfirmTourService()
        {
            _repository = new ConfirmTourRepository();
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }


        public List<ConfirmTour> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(ConfirmTour tour)
        {
            _repository.Save(tour);
        }

        public ConfirmTour? Update(ConfirmTour entity)
        {
           
            return _repository.Update(entity);
        }

        public void Delete(ConfirmTour entity)
        {
            _repository.Delete(entity);
           
        }


        public void loadGuests(UserRepository userRepository)
        {
            foreach (ConfirmTour tour in _repository.GetAll())
            {
                tour.User = userRepository.GetById(tour.UserId);
            }
        }


        public List<User> GetGuestOnTour(Tour selectedTour)
        {
            List<User> GuestOnTour = new List<User>();

            foreach (ConfirmTour tour in _repository.GetAll())
            {
                if (tour.IdTour == selectedTour.getID())
                {
                    if (tour.IdCheckpoint < 0)
                        GuestOnTour.Add(tour.User);
                }
            }
            return GuestOnTour;

        }

    }
}
