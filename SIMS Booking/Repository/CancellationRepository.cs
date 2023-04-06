using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class CancellationRepository : Repository<Reservation>, ISubject
    {
        public CancellationRepository() : base("../../../Resources/Data/cancellations.csv") { }
    }
}
