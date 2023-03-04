using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class AccomodationRepository : Repository<Accommodation>
    {       
        public AccomodationRepository() : base("../../../Resources/Data/accommodations.csv") { }
    }
}
