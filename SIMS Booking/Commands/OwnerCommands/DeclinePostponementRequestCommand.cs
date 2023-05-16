using SIMS_Booking.Enums;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class DeclinePostponementRequestCommand : CommandBase
    {
        private readonly DeclinePostponementRequestViewModel _viewModel;
        private readonly PostponementService _postponementService;
        private readonly INavigationService _navigationService;

        public DeclinePostponementRequestCommand(INavigationService navigationService, DeclinePostponementRequestViewModel viewModel, PostponementService postponementService)
        {
            _viewModel = viewModel;
            _postponementService = postponementService;
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            PostponementStatus postponementStatus = PostponementStatus.Declined;
            _postponementService.ReviewPostponementRequest(_viewModel.SelectedRequest.GetId(), _viewModel.Comment, postponementStatus);
            _navigationService.Navigate();
        }
    }
}
