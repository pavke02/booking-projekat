using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using System.Windows.Controls;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.Guest1Commands;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Commands.NavigateCommands;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    public class Guest1ChangeReservationViewModel : ViewModelBase
    {
        private readonly Reservation _selectedReservation;
        public User LoggedUser { get; set; }
        private ReservationService _reservationService;
        public List<Reservation> Reservations { get; set; }
        public List<Reservation> AccommodationReservations { get; set; }
        private ReservedAccommodationService _reservedAccommodationService;
        private PostponementService _postponementService;

        private string _minDaysContent;

        public string MinDaysContent
        {
            get => _minDaysContent;
            set
            {
                if (value != _minDaysContent)
                {
                    _minDaysContent = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuestsContent;

        public string MaxGuestsContent
        {
            get => _maxGuestsContent;
            set
            {
                if (value != _maxGuestsContent)
                {
                    _maxGuestsContent = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime _startDpStartDate;

        public DateTime StartDpStartDate
        {
            get => _startDpStartDate;
            set
            {
                if (value != _startDpStartDate)
                {
                    _startDpStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDpDisplayStartDate;

        public DateTime EndDpDisplayStartDate
        {
            get => _endDpDisplayStartDate;
            set
            {
                if (value != _endDpDisplayStartDate)
                {
                    _endDpDisplayStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endDpDisplayEndDate;

        public DateTime EndDpDisplayEndDate
        {
            get => _endDpDisplayEndDate;
            set
            {
                if (value != _endDpDisplayEndDate)
                {
                    _endDpDisplayEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _selectedStartDate;

        public DateTime SelectedStartDate
        {
            get => _selectedStartDate;
            set
            {
                if (value != _selectedStartDate)
                {
                    _selectedStartDate = value;
                    OnPropertyChanged();
                    EndDpDateStartChangedEvent?.Invoke(_selectedReservation.Accommodation.MinReservationDays);
                }
            }
        }



        private DateTime _selectedEndDate;

        public DateTime SelectedEndDate
        {
            get => _selectedEndDate;
            set
            {
                if (value != _selectedEndDate)
                {
                    _selectedEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _endDpEnabled;
        public bool EndDpEnabled
        {
            get => _endDpEnabled;
            set
            {
                if (value != _endDpEnabled)
                {
                    _endDpEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateBackCommand { get; }

        public ICommand PostponeReservationCommand { get; }


        public Guest1ChangeReservationViewModel(ModalNavigationStore modalNavigationStore, Reservation selectedReservation, User loggedUser, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, PostponementService postponementService)
        {
            _selectedReservation = selectedReservation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            _postponementService = postponementService;
            LoggedUser = loggedUser;

            int minimumDaysOfReservation = _selectedReservation.Accommodation.MinReservationDays;
            MinDaysContent = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
            int maxGuests = _selectedReservation.Accommodation.MaxGuests;
            MaxGuestsContent = $"Maximum number of guests: {maxGuests} guests.";

            StartDpStartDate = DateTime.Today.AddDays(1);
            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
            PostponeReservationCommand =
                new PostponeCommand(CreateCloseModalNavigationService(modalNavigationStore), _postponementService, _reservationService, _selectedReservation, this);

            Reservations = _reservationService.GetAll();
            AccommodationReservations = _reservationService.GetAccommodationReservations(_selectedReservation.Accommodation);

            foreach (Reservation reservation in AccommodationReservations)
            {
                if (reservation.GetId() == selectedReservation.GetId())
                {
                    AccommodationReservations.Remove(reservation);
                    break;
                }
            }

            /*DisableReservedDates(AccommodationReservations, startDateDp, endDateDp);
            DisableAllImpossibleDates(startDateDp, minimumDaysOfReservation);*/
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (var date in BlackoutDatesChangedEvent.GetInvocationList())
            {
                BlackoutDatesChangedEvent -= date as BlackoutDatesChangedHandler;
                EndDpDateStartChangedEvent -= date as EndDpDateStartChangeHandler;
            }

        }

        public delegate void BlackoutDatesChangedHandler(List<CalendarDateRange> reservedDates);
        public event BlackoutDatesChangedHandler? BlackoutDatesChangedEvent;
        public delegate void EndDpDateStartChangeHandler(int minDays);
        public event EndDpDateStartChangeHandler? EndDpDateStartChangedEvent;

        public void DisableReservedDates()
        {
            List<CalendarDateRange> blackoutDates = new List<CalendarDateRange>();
            foreach (var reservation in _reservationService.GetAccommodationReservations(_selectedReservation.Accommodation))
            {
                var startDate = reservation.StartDate.Date;
                var endDate = reservation.EndDate.Date;

                var range = new CalendarDateRange(startDate, endDate);

                blackoutDates.Add(range);
            }

        
            BlackoutDatesChangedEvent?.Invoke(blackoutDates);
        }

        /*private void DisableReservedDates(List<Reservation> accommodationReservations, DatePicker startDatePicker, DatePicker endDatePicker)
        {
            foreach (var reservation in accommodationReservations)
            {
                var startDate = reservation.StartDate.Date;
                var endDate = reservation.EndDate.Date;

                var range = new CalendarDateRange(startDate, endDate);
                if (startDatePicker.SelectedDate >= startDate && startDatePicker.SelectedDate <= endDate)
                {
                    startDatePicker.SelectedDate = endDate.AddDays(1);
                }
                startDatePicker.BlackoutDates.Add(range);
                if (endDatePicker.SelectedDate >= startDate && endDatePicker.SelectedDate <= endDate)
                {
                    endDatePicker.SelectedDate = endDate.AddDays(1);
                }
                endDatePicker.BlackoutDates.Add(range);
            }

        }

        public void DisableAllImpossibleDates(DatePicker datePicker, int minimumReservationDays)
        {
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(1 + minimumReservationDays);
            CalendarBlackoutDatesCollection blackoutRanges = datePicker.BlackoutDates;
            List<CalendarDateRange> rangesToDelete = new List<CalendarDateRange>();
            foreach (CalendarDateRange blackoutRange in blackoutRanges)
            {
                CalendarDateRange rangeToDelete =
                    new CalendarDateRange(blackoutRange.Start.AddDays(-(minimumReservationDays)),
                        blackoutRange.Start.AddDays(-1));
                rangesToDelete.Add(rangeToDelete);
            }

            datePicker.SelectedDate = null;
            foreach (CalendarDateRange rangeToDelete in rangesToDelete)
            {
                datePicker.BlackoutDates.Add(rangeToDelete);
            }

        }

        private void StartDateDpSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (startDateDp.SelectedDate.HasValue)
            {
                if (!endDateDp.IsEnabled)
                {
                    endDateDp.IsEnabled = true;
                }
                DateTime? minimumEndDate = startDateDp.SelectedDate.Value.AddDays(_selectedReservation.Accommodation.MinReservationDays);
                endDateDp.DisplayDateStart = minimumEndDate;

                endDateDp.SelectedDate = minimumEndDate;
                endDateDp.DisplayDateEnd = GetFirstBlackoutDateAfterDate(endDateDp, startDateDp.SelectedDate.Value);
            }
        }

        public DateTime? GetFirstBlackoutDateAfterDate(DatePicker datePicker, DateTime date)
        {
            var blackoutDates = datePicker.BlackoutDates;

            var nextBlackoutDate = blackoutDates.FirstOrDefault(d => d.Start > date);

            if (nextBlackoutDate == null)
            {
                return null;
            }

            return nextBlackoutDate.Start.AddDays(-1);
        }*/
    }
}
