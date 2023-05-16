using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Common;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class SubmitOwnerReviewCommand : CommandBase
    {
        private readonly Guest1OwnerReviewViewModel _viewModel;

        private readonly OwnerReviewService _ownerReviewService;

        private readonly ReservationService _reservationService;

        private readonly INavigationService _closeModalNavigationService;

        public SubmitOwnerReviewCommand(INavigationService closeModalNavigationService, Guest1OwnerReviewViewModel viewModel, OwnerReviewService ownerReviewService, ReservationService reservationService)
        {
            _viewModel = viewModel;
            _ownerReviewService = ownerReviewService;
            _reservationService = reservationService;
            _closeModalNavigationService = closeModalNavigationService;
        }

        public override void Execute(object? parameter)
        {
            List<string> imageURLs = new List<string>();
            string[] values = _viewModel.ImageURLs.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);

            if (!_viewModel.RenovationEnabled)
                _ownerReviewService.SubmitReview(_viewModel.Tidiness, _viewModel.OwnerFairness, _viewModel.Comment, _viewModel._reservation, imageURLs, false, 0, "");
            else
                _ownerReviewService.SubmitReview(_viewModel.Tidiness, _viewModel.OwnerFairness, _viewModel.Comment, _viewModel._reservation, imageURLs, true, _viewModel.RenovationLevel, _viewModel.RenovationComment);
            _reservationService.Update(_viewModel._reservation);
            _closeModalNavigationService.Navigate();
        }
    }
}
