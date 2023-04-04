using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class AccommodationRepository : Repository<Accommodation>, ISubject
    {       
        public AccommodationRepository() : base("../../../Resources/Data/accommodations.csv") { }    
    }
}
