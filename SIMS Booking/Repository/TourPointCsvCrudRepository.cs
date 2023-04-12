using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class TourPointCsvCrudRepository : CsvCrudRepository<TourPoint>,ISubject
    {

        public TourPointCsvCrudRepository() : base("../../../Resources/Data/checkpoints.csv") { }



    }
}
