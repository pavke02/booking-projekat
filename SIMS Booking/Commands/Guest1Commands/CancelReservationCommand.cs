using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System.Windows;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class CancelReservationCommand : CommandBase
    {
        private readonly ReservationService _reservationService;
        private readonly Guest1MainViewModel _viewModel;
        private readonly PostponementService _postponementService;

        public CancelReservationCommand(ReservationService reservationService,  Guest1MainViewModel viewModel)
        {
            _reservationService = reservationService;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to cancel this reservation", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (_viewModel.SelectedReservation.StartDate - DateTime.Today <
                    TimeSpan.FromDays(_viewModel.SelectedReservation.Accommodation.CancellationPeriod))
                {
                    MessageBox.Show("It is not possible to cancel reservation after cancellation period.");
                    return;
                }
                List<Reservation> newReservations = _reservationService.GetAll();
                foreach (Reservation reservation in newReservations)
                {
                    if (reservation.GetId() == _viewModel.SelectedReservation.GetId())
                    {
                        reservation.IsCanceled = true;
                        _reservationService.Update(reservation);
                        _viewModel.SetSuperGuest();
                        return;
                    }
                }
            }
        }
    }
}
