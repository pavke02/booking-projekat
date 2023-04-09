using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class TourCsvCrudRepository : CsvCrudRepository<Tour> , ISubject
    {

        public TourCsvCrudRepository() : base("../../../Resources/Data/guides.csv") { }        

    }
}