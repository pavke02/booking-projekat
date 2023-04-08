using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class ReservationCsvCrudRepository : CsvCrudRepository<Reservation>, ISubject
    {
        public ReservationCsvCrudRepository() : base("../../../Resources/Data/reservations.csv") { }        
    }
}
