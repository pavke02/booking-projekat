using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class CancellationCsvCrudRepository : CsvCrudRepository<Reservation>
    {
        public CancellationCsvCrudRepository() : base("../../../Resources/Data/cancellations.csv") { }
    }
}
