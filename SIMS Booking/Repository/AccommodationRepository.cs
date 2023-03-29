using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class AccommodationRepository : Repository<Accommodation>, ISubject
    {       
        public AccommodationRepository() : base("../../../Resources/Data/accommodations.csv") { }    
    }
}
