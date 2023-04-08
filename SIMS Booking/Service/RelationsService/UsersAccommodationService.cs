using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.Service.RelationsService
{
    public class UsersAccommodationService
    {
        private readonly UsersAccommodationCsvCrudRepository _csvCrudRepository;

        public UsersAccommodationService()
        {
            _csvCrudRepository = new UsersAccommodationCsvCrudRepository();
        }

        public void Save(UsersAccommodation usersAccommodation)
        {
            _csvCrudRepository.Save(usersAccommodation);
        }

        public void LoadUsersInAccommodation(UserService userService, AccommodationService accommodationService)
        {
            foreach (UsersAccommodation usersAccommodation in _csvCrudRepository.GetAll())
            {
                foreach (Accommodation accommodation in accommodationService.GetAll())
                {
                    if (usersAccommodation.AccommodationId == accommodation.getID())
                        accommodation.User = userService.GetById(usersAccommodation.UserId);
                }
            }
        }
    }
}
