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
        

        public List<string> GetGuestOnTour(Tour selectedTour)
        {
            List<string> GuestOnTour = new List<string>();
            bool flag = false;
            foreach (ConfirmTour tour in _entityList)
            {
                if (tour.IdTour == selectedTour.ID)
                {
                    foreach (TourPoint tourPoint in selectedTour.TourPoints)
                    {
                        if (tour.IdCheckpoint == tourPoint.getID())
                        {
                            GuestOnTour.Add(tour.Name);
                            flag = true;
                            
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                        
                }
            }
            return GuestOnTour;


        }
    }
}

