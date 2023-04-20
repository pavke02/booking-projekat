using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class SubmitReviewCommand : CommandBase
    {
        private readonly GuestReviewViewModel _viewModel;
        private readonly GuestReviewService _guestReviewService;
        private readonly ReservationService _reservationService;
        private readonly Reservation _reservation;
        private readonly INavigationService _closeModalNavigationService;

        public SubmitReviewCommand(INavigationService closeModalNavigationService, GuestReviewViewModel viewModel, GuestReviewService guestReviewService,
            ReservationService reservationService, Reservation reservation)
        {
            _viewModel = viewModel;
            _closeModalNavigationService = closeModalNavigationService;

            _guestReviewService = guestReviewService;
            _reservationService = reservationService;
            _reservation = reservation;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ErrorText = string.IsNullOrEmpty(_viewModel.Comment);
            if (_viewModel.ErrorText) return;

            _guestReviewService.SubmitReview(_viewModel.Tidiness, _viewModel.RuleFollowing, _viewModel.Comment, _reservation);
            _reservationService.Update(_reservation);

            _closeModalNavigationService.Navigate();
        }
    }
}
