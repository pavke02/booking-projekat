using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.Service.RelationsService
{
    public class UsersAccommodationService
    {
        private readonly UsersAccommodationRepository _repository;

        public UsersAccommodationService()
        {
            _repository = new UsersAccommodationRepository();
        }

        public void Save(UsersAccommodation usersAccommodation)
        {
            _repository.Save(usersAccommodation);
        }

        public void LoadUsersInAccommodation(UserService userService, AccommodationService accommodationService)
        {
            foreach (UsersAccommodation usersAccommodation in _repository.GetAll())
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
