using SIMS_Booking.Model;

namespace SIMS_Booking.Repository
{
    public class AccomodationRepository : Repository<Accommodation>
    {       
        public AccomodationRepository() : base("../../../Resources/Data/accommodations.csv") { }
    }
}
