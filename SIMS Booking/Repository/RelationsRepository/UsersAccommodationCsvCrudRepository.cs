using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class UsersAccommodationCsvCrudRepository: RelationsCsvCrudRepository<UsersAccommodation>
    {
        public UsersAccommodationCsvCrudRepository() : base("../../../Resources/Data/usersAccommodation.csv") { }
    }
}
