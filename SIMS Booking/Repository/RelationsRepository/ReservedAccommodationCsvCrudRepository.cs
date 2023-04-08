using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ReservedAccommodationCsvCrudRepository : RelationsCsvCrudRepository<ReservedAccommodation>
    {
        public ReservedAccommodationCsvCrudRepository() : base("../../../Resources/Data/reservedAccommodations.csv") { }     
    }
}
