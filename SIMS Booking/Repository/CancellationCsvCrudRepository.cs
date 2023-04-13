using SIMS_Booking.Model;

namespace SIMS_Booking.Repository
{
    public class CancellationCsvCrudRepository : CsvCrudRepository<Reservation>
    {
        public CancellationCsvCrudRepository() : base("../../../Resources/Data/cancellations.csv") { }
    }
}
