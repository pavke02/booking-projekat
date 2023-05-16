using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    class ResetCommand : CommandBase
    {
        private readonly OwnerMainViewModel _viewModel;

        public ResetCommand(OwnerMainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.AccommodationName = "";
            _viewModel.MaxGuests = "";
            _viewModel.MinReservationDays = "";
            _viewModel.CancellationPeriod = "";
            _viewModel.ImageURLs = "";
            _viewModel.AccommodationType = null;
            _viewModel.City = null;
        }
    }
}
