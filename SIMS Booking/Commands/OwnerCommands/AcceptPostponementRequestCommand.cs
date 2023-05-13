using SIMS_Booking.Enums;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands
{
    public class AcceptPostponementRequestCommand : CommandBase
    {
        private readonly PostponeReservationViewModel _viewModel;
        private readonly ReservationService _reservationService;
        private readonly PostponementService _postponementService;

        public AcceptPostponementRequestCommand(PostponeReservationViewModel viewModel, ReservationService reservationService, PostponementService postponementService)
        {
            _viewModel = viewModel;
            _reservationService = reservationService;
            _postponementService = postponementService;
        }

        public override void Execute(object? parameter)
        {
            _reservationService.PostponeReservation(_viewModel.SelectedRequest.ReservationId, _viewModel.SelectedRequest.NewStartDate,
                _viewModel.SelectedRequest.NewEndDate);
            string comment = "";
            PostponementStatus postponementStatus = PostponementStatus.Accepted;
            _postponementService.ReviewPostponementRequest(_viewModel.SelectedRequest.GetId(), comment, postponementStatus);
            _viewModel.IsVisible = false;
        }
    }
}
