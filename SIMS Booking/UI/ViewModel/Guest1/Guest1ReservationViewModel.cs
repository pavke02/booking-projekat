using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.View.Guest1;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Utility.Stores;
using SIMS_Booking.Commands.NavigateCommands;
using System.Windows.Input;
using SIMS_Booking.Commands.Guest1Commands;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    public class Guest1ReservationViewModel : ViewModelBase
    {
        public User LoggedUser { get; set; }
        private ReservationService _reservationService;
        public List<Reservation> Reservations { get; set; }
        public List<Reservation> AccommodationReservations { get; set; }
        private ReservedAccommodationService _reservedAccommodationService;

        private Accommodation _selectedAccommodation;

        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string _guestNumberTb;

        public string GuestNumberTb
        {
            get => _guestNumberTb;
            set
            {
                if (value != _guestNumberTb)
                {
                    _guestNumberTb = value;
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
                    EndDpDateStartChangedEvent?.Invoke(_selectedAccommodation.MinReservationDays);
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
        public ICommand ReserveCommand { get; }

        public Guest1MainViewModel ViewModel;
        public Guest1WheneverWhereverViewModel ViewModel2;

        public Guest1ReservationViewModel(ModalNavigationStore modalNavigationStore, Accommodation selectedAccommodation, User loggedUser, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, Guest1MainViewModel guest1MainViewModel, Guest1WheneverWhereverViewModel viewModel2)
        {

            _selectedAccommodation = selectedAccommodation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            LoggedUser = loggedUser;
            ViewModel = guest1MainViewModel;

            int minimumDaysOfReservation = _selectedAccommodation.MinReservationDays;
            MinDaysContent = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
            int maxGuests = _selectedAccommodation.MaxGuests;
            MaxGuestsContent = $"Maximum number of guests: {maxGuests} guests.";

            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));

            StartDpStartDate = DateTime.Today.AddDays(1);
            SelectedStartDate = DateTime.Today.AddDays(1);

            ReserveCommand = new ReserveCommand(CreateCloseModalNavigationService(modalNavigationStore), _selectedAccommodation, _reservationService,
                _reservedAccommodationService, LoggedUser, this);

            Reservations = _reservationService.GetAll();
            AccommodationReservations = _reservationService.GetAccommodationReservations(selectedAccommodation);
            ViewModel2 = viewModel2;
        }

        public Guest1ReservationViewModel(ModalNavigationStore modalNavigationStore, Accommodation selectedAccommodation, User loggedUser, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, Guest1WheneverWhereverViewModel guest1WhereverViewModelViewModel)
        {

            _selectedAccommodation = selectedAccommodation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            LoggedUser = loggedUser;

            int minimumDaysOfReservation = _selectedAccommodation.MinReservationDays;
            MinDaysContent = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
            int maxGuests = _selectedAccommodation.MaxGuests;
            MaxGuestsContent = $"Maximum number of guests: {maxGuests} guests.";

            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));

            StartDpStartDate = DateTime.Today.AddDays(1);
            SelectedStartDate = DateTime.Today.AddDays(1);

            ReserveCommand = new ReserveCommand(CreateCloseModalNavigationService(modalNavigationStore), _selectedAccommodation, _reservationService,
                _reservedAccommodationService, LoggedUser, this);

            Reservations = _reservationService.GetAll();
            AccommodationReservations = _reservationService.GetAccommodationReservations(selectedAccommodation);
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
            foreach (var reservation in _reservationService.GetAccommodationReservations(_selectedAccommodation))
            {
                var startDate = reservation.StartDate.Date;
                var endDate = reservation.EndDate.Date;

                var range = new CalendarDateRange(startDate, endDate);

                blackoutDates.Add(range);
            }

            BlackoutDatesChangedEvent?.Invoke(blackoutDates);
        }


        /*private void Reserve(object sender, RoutedEventArgs e)
        {

            if (_selectedAccommodation.MaxGuests < Convert.ToInt32(GuestNumberTb))
            {
                MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this accommodation ({_selectedAccommodation.MaxGuests} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Reservation reservation = new Reservation(SelectedStartDate, SelectedEndDate, _selectedAccommodation, LoggedUser, false, false, false, false);
            _reservationService.Save(reservation);

            ReservedAccommodation reservedAccommodation = new ReservedAccommodation(LoggedUser.getID(), _selectedAccommodation.getID(), reservation.getID());
            _reservedAccommodationService.Save(reservedAccommodation);

        }

        private void DisableReservedDates(List<Reservation> accommodationReservations)
        {
            foreach (var reservation in accommodationReservations)
            {
                var startDate = reservation.StartDate.Date;
                var endDate = reservation.EndDate.Date;

                var range = new CalendarDateRange(startDate, endDate);
                if (SelectedStartDate >= startDate && SelectedStartDate <= endDate)
                {
                    SelectedStartDate = endDate.AddDays(1);
                }
                startDatePicker.BlackoutDates.Add(range);
                if (SelectedEndDate >= startDate && SelectedEndDate <= endDate)
                {
                    SelectedEndDate = endDate.AddDays(1);
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

                if (!EndDpEnabled)
                {
                    EndDpEnabled = true;
                }
                DateTime minimumEndDate = SelectedStartDate.AddDays(_selectedAccommodation.MinReservationDays);
                EndDpDisplayStartDate = minimumEndDate;

                SelectedEndDate = minimumEndDate;
                EndDpDisplayEndDate = GetFirstBlackoutDateAfterDate(endDateDp, SelectedStartDate);
            
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
