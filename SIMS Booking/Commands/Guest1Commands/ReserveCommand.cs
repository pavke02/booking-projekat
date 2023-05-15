using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using System.Windows;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Guest1;

namespace SIMS_Booking.Commands.Guest1Commands
{
    public class ReserveCommand : CommandBase
    {

        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly Guest1ReservationViewModel _viewModel;
        private readonly Accommodation _selectedAccommodation;
        private readonly User _user;

        public ReserveCommand(Accommodation selectedAccommodation, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, User user, Guest1ReservationViewModel viewModel)
        {
            _selectedAccommodation = selectedAccommodation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            _user = user;
            _viewModel = viewModel;
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
        }
    }
}
