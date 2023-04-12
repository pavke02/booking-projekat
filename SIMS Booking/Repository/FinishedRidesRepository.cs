using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository.RelationsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Repository
{
    public class FinishedRidesRepository : CsvCrudRepository<FinishedRide>, ISubject
    {
        public FinishedRidesRepository() : base("../../../Resources/Data/finishedRides.csv") { }
    }
}
