using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class ConfirmTourCsvCrudRepository : CsvCrudRepository<ConfirmTour>
    {
        public ConfirmTourCsvCrudRepository() : base("../../../Resources/Data/confirmTours.csv") { }
    }
}

