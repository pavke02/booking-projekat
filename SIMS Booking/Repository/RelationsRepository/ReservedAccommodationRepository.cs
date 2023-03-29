using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ReservedAccommodationRepository : RelationsRepository<ReservedAccommodation>
    {
        public ReservedAccommodationRepository() : base("../../../Resources/Data/reservedAccommodations.csv") { }     
    }
}
