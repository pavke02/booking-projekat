using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using SIMS_Booking.View;

namespace SIMS_Booking.Repository
{
    public class TourRepository : Repository<Tour> , ISubject
    {

        public TourRepository() : base("../../../Resources/Data/guides.csv") { }
    }
}