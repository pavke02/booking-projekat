using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{

    public class NumberOfGuestsOnTourRepository : Repository<NumberOfGuestsOnTour>, ISubject
    {
        public NumberOfGuestsOnTourRepository() : base("../../../Resources/Data/numberOfGuestsOnTour.csv") { }

    }
}
