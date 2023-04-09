using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Service;

namespace SIMS_Booking.Repository
{
    public class ConfirmTourCsvCrudRepository : CsvCrudRepository<ConfirmTour>, ISubject
    {
        public ConfirmTourCsvCrudRepository() : base("../../../Resources/Data/confirmTours.csv") { }
      
        public void loadGuests (UserService userService)
        {
            foreach(ConfirmTour tour in _entityList)
            {
                tour.User = userService.GetById(tour.UserId);
            }
        }


        public List<User> GetGuestOnTour(Tour selectedTour)
        {
            List<User> GuestOnTour = new List<User>();
           
            foreach (ConfirmTour tour in _entityList)
            {
                if (tour.IdTour == selectedTour.getID())
                {
                    if(tour.IdCheckpoint < 0)
                    GuestOnTour.Add(tour.User);          
                }
            }
            return GuestOnTour;

        }
    }
}

