using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class AccommodationCsvCrudRepository : CsvCrudRepository<Accommodation>, ISubject
    {       
        public AccommodationCsvCrudRepository() : base("../../../Resources/Data/accommodations.csv") { }    
    }
}
