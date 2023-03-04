using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class AccomodationRepository : Repository<Accommodation>, ISubject
    {       
        public AccomodationRepository() : base("../../../Resources/Data/accommodations.csv") { }        
    }
}
