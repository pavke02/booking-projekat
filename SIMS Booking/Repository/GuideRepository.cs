using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Repository
{
    public class GuideRepository : Repository<Guide>
    {
        public GuideRepository() : base("../../../Resources/Data/guides.csv") { }    
    }
}
