using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class UsersAccommodationRepository: RelationsRepository<UsersAccommodation>
    {
        public UsersAccommodationRepository() : base("../../../Resources/Data/usersAccommodation.csv") { }
    }
}
