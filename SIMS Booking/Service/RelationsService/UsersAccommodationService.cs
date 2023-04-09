using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;

namespace SIMS_Booking.Service.RelationsService
{
    public class UsersAccommodationService
    {
        private readonly RelationsCrudService<UsersAccommodation> _crudService;

        public UsersAccommodationService()
        {
            _crudService = new RelationsCrudService<UsersAccommodation>("../../../Resources/Data/usersAccommodation.csv");
        }

        public void Save(UsersAccommodation usersAccommodation)
        {
            _crudService.Save(usersAccommodation);
        }

        public void LoadUsersInAccommodation(UserService userService, AccommodationService accommodationService)
        {
            foreach (UsersAccommodation usersAccommodation in _crudService.GetAll())
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
