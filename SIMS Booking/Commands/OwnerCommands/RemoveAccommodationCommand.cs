using System.Diagnostics;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class RemoveAccommodationCommand: CommandBase
    {
        private readonly RemoveAccommodationOnLocationViewModel _viewModel;

        private readonly AccommodationService _accommodationService;
        private readonly UsersAccommodationService _usersAccommodationService;

        private Accommodation _unpopularAccommodation;

        public RemoveAccommodationCommand(AccommodationService accommodationService,
            UsersAccommodationService usersAccommodationService, RemoveAccommodationOnLocationViewModel viewModel)
        {
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;

            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            //ToDo: implement
            Trace.WriteLine("Uradi");
            Trace.Write(_viewModel.SelectedAccommodation.Name);
        }
    }
}
