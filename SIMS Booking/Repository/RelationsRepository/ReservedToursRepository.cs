using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public  class ReservedToursRepository : RelationsRepository<TourReservation>
    {

        public ReservedToursRepository() : base("../../../Resources/Data/reservedTours.csv") { }








    }
}
