using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service.RelationsService
{
    public class UsersAccommodationService
    {
        private readonly ICRUDRepository<UsersAccommodation> _repository;

        public UsersAccommodationService(ICRUDRepository<UsersAccommodation> repository)
        {
            _repository = repository;
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
                    if (usersAccommodation.AccommodationId == accommodation.GetId())
                        accommodation.User = userService.GetById(usersAccommodation.UserId);
                }
            }
        }
    }
}
