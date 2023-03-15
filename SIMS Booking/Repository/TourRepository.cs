using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Repository
{
    public class TourRepository : Repository<Tour> , ISubject
    {

        public TourRepository() : base("../../../Resources/Data/guides.csv") { }

        public List<Tour> GetTodaysTours()
        {
            List<Tour> todaysTours = new List<Tour>();
            foreach (Tour tour in _entityList)
            {
                if (DateTime.Today == tour.StartTour)
                {
                    todaysTours.Add(tour);
                    
                }
            }
            return todaysTours;
        }

       

    }
}
