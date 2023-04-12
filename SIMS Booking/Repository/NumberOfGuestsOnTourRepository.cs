using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{

    public class NumberOfGuestsOnTourRepository : CsvCrudRepository<NumberOfGuestsOnTour>, ISubject
    {
        public NumberOfGuestsOnTourRepository() : base("../../../Resources/Data/numberOfGuestsOnTour.csv") { }

    }
}
