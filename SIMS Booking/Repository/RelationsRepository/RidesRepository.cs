using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class RidesRepository : RelationsRepository<Rides>
    {
        public RidesRepository() : base("../../../Resources/Data/rides.csv") { }
    }
}
