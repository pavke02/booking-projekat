using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    internal class ClearURLsCommand : CommandBase
    {
        private readonly IPublish _viewModel;

        public ClearURLsCommand(IPublish viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ImageURLs = "";
        }
    }
}
