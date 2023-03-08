using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Repository
{
    public class TourRepository : Repository<Tour>
    {

        public TourRepository() : base("../../../Resources/Data/guides.csv") { }    

    }
}
