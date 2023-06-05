using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using Microsoft.VisualStudio.Services.Profile;
using SIMS_Booking.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Common;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class ApplyWheneverCommand : CommandBase
    {
        private readonly Guest1WheneverWhereverViewModel _viewModel;
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;

        public ApplyWheneverCommand(Guest1WheneverWhereverViewModel viewModel, AccommodationService accommodationService, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService)
        {
            _viewModel = viewModel;
            _accommodationService = accommodationService;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
        }

        public override void Execute(object? parameter)
        {
            int maxGuests = Convert.ToInt32(_viewModel.MaxGuests);
            int minDays = Convert.ToInt32(_viewModel.MinReservationDays);
            DateTime startDate = _viewModel.StartSelectedDate;
            DateTime endDate = _viewModel.EndSelectedDate;

            ObservableCollection<Accommodation> accommodationsFiltered = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            _viewModel.Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());

            foreach (Accommodation accommodation in _viewModel.Accommodations)
            {
                bool fitsFilter = FitsWheneverFilter(_reservationService, accommodation, maxGuests, minDays, startDate, endDate);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(_viewModel.Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            _viewModel.Accommodations = accommodationsFiltered;
        }

        bool FitsWheneverFilter(ReservationService reservationService, Accommodation accommodation, int maxGuests,
            int minDays, DateTime startDate, DateTime endDate)
        {
            if (accommodation.MaxGuests < maxGuests)
                return false;
            if (accommodation.MinReservationDays > minDays)
                return false;

            DateTime tempDate = startDate;
            int currentStreak = 0;
            int longestStreak = 0;
            while (tempDate != endDate)
            {
                foreach (Reservation reservation in reservationService.GetAccommodationReservations(accommodation))
                {
                    if (tempDate >= reservation.StartDate && tempDate <= reservation.EndDate)
                    {
                        if (longestStreak < currentStreak)
                        {
                            longestStreak = currentStreak;
                        }
                        currentStreak = 0;
                    }

                    currentStreak++;
                }

                tempDate = tempDate.AddDays(1);
            }

            if (longestStreak < minDays && !reservationService.GetAccommodationReservations(accommodation).IsNullOrEmpty())
                return false;
            return true;
        }

    }
}
