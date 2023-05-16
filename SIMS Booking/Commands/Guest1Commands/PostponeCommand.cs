using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using System.Windows;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class PostponeCommand : CommandBase
    {
        private readonly PostponementService _postponementService;
        private readonly ReservationService _reservationService;
        private readonly Guest1ChangeReservationViewModel _viewModel;
        private readonly Reservation _selectedReservation;
        private readonly INavigationService _closeModalNavigationService;

        public PostponeCommand(INavigationService closeModalNavigationService, PostponementService postponementService, ReservationService reservationService, Reservation selectedReservation, Guest1ChangeReservationViewModel viewModel)
        {
            _postponementService = postponementService;
            _reservationService = reservationService;
            _viewModel = viewModel;
            _selectedReservation = selectedReservation;
            _closeModalNavigationService = closeModalNavigationService;
        }

        public override void Execute(object? parameter)
        {
            Postponement postponement = new Postponement(_reservationService.GetById(_selectedReservation.GetId()), _viewModel.SelectedStartDate, _viewModel.SelectedEndDate, PostponementStatus.Pending, false);
            _postponementService.Save(postponement);
            MessageBox.Show("Request sent successfully");
            _closeModalNavigationService.Navigate();
        }
    }
}
