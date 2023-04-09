using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.Repository
{
    public class RidesRepository : CsvCrudRepository<Rides>
    {
        public RidesRepository() : base("../../../Resources/Data/rides.csv") { }
    }
}
