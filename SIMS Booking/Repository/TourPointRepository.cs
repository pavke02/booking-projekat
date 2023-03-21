using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class TourPointRepository : Repository<TourPoint>,ISubject
    {

        public TourPointRepository() : base("../../../Resources/Data/checkpoints.csv") { }



    }
}
