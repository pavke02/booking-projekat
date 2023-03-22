using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class UsersAccommodationRepository: RelationsRepository<UsersAccommodation>
    {
        public UsersAccommodationRepository() : base("../../../Resources/Data/usersAccommodation.csv") { }

        public void LoadUsersInAccommodation(UserRepository userRepository, AccomodationRepository accomodationRepository)
        {
            foreach (UsersAccommodation usersAccommodation in _entityList)
            {
                foreach (Accommodation accommodation in accomodationRepository.GetAll())
                {
                    if (usersAccommodation.AccommodationId == accommodation.getID())
                        accommodation.User = userRepository.GetById(usersAccommodation.UserId);
                }                    
            }                
        }
    }
}
