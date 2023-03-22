using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.View;

namespace SIMS_Booking.Repository
{
    public class ConfirmTourRepository : Repository<ConfirmTour>, ISubject
    {
        public ConfirmTourRepository() : base("../../../Resources/Data/confirmTours.csv") { }
      
        public void loadGuests (UserRepository userRepository)
        {
            foreach(ConfirmTour tour in _entityList)
            {
                tour.User = userRepository.GetById(tour.UserId);
            }
        }


        public List<User> GetGuestOnTour(Tour selectedTour)
        {
            List<User> GuestOnTour = new List<User>();
           
            foreach (ConfirmTour tour in _entityList)
            {
                if (tour.IdTour == selectedTour.ID)
                {
                    if(tour.IdCheckpoint < 0)
                    GuestOnTour.Add(tour.User);          
                }
            }
            return GuestOnTour;

        }
    }
}

