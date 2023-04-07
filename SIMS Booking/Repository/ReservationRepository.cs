using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class ReservationRepository : Repository<Reservation>, ISubject
    {
        public ReservationRepository() : base("../../../Resources/Data/reservations.csv") { }        
    }
}
