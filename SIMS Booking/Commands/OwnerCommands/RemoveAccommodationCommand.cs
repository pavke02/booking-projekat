using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class RemoveAccommodationCommand: CommandBase
    {
        private readonly AccommodationService _accommodationService;
        private readonly UsersAccommodationService _usersAccommodationService;

        private Accommodation _unpopularAccommodation;

        public RemoveAccommodationCommand(AccommodationService accommodationService,
            UsersAccommodationService usersAccommodationService, Accommodation unpopularAccommodation)
        {
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;
            _unpopularAccommodation = unpopularAccommodation;
        }

        public override void Execute(object? parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
