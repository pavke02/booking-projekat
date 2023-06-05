using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Windows;

namespace SIMS_Booking.Commands.Guest1Commands
{
    public class ReserveCommand : CommandBase
    {

        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly Guest1ReservationViewModel _viewModel;
        private readonly Accommodation _selectedAccommodation;
        private readonly User _user;
        private readonly INavigationService _closeModalNavigationService;

        public ReserveCommand(INavigationService closeModalNavigationService, Accommodation selectedAccommodation, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, User user, Guest1ReservationViewModel viewModel)
        {
            _selectedAccommodation = selectedAccommodation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            _user = user;
            _viewModel = viewModel;
            _closeModalNavigationService = closeModalNavigationService;
        }

        public override void Execute(object? parameter)
        {
            if (_selectedAccommodation.MaxGuests < Convert.ToInt32(_viewModel.GuestNumberTb))
            {
                MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this accommodation ({_selectedAccommodation.MaxGuests} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Reservation reservation = new Reservation(_viewModel.SelectedStartDate, _viewModel.SelectedEndDate, _selectedAccommodation, _user, false, false, false, false);
            _reservationService.Save(reservation);

            ReservedAccommodation reservedAccommodation = new ReservedAccommodation(_user.GetId(), _selectedAccommodation.GetId(), reservation.GetId());
            _reservedAccommodationService.Save(reservedAccommodation);
            if (_viewModel.ViewModel != null)
            {
                _viewModel.ViewModel.SetSuperGuest();
            }
            if(_viewModel.ViewModel2 != null)
            {
                _viewModel.ViewModel2.SetSuperGuest();
            }

            _closeModalNavigationService.Navigate();
            MessageBox.Show("Accommodation successfully reserved!");
        }
    }
}
