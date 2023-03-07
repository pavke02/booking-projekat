using SIMS_Booking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Repository
{
    public class ReservationRepository : Repository<Reservation>
    {
        public ReservationRepository() : base("../../../Resources/Data/reservations.csv") { }
    }
}
