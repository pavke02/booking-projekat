using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class PostponementCsvCrudRepository : CsvCrudRepository<Postponement>, ISubject
    {
        public PostponementCsvCrudRepository() : base("../../../Resources/Data/postponements.csv") { }
    }
}
