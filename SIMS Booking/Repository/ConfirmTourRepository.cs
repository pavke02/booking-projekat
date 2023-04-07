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
      
       
    }
}

