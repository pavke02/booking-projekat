using SIMS_Booking.Model;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;

namespace SIMS_Booking.View
{
    public partial class Guest1ChangeReservationView : Window
    {
        private readonly Reservation _selectedReservation;
        public User LoggedUser { get; set; }
        private ReservationService _reservationService;
        public List<Reservation> Reservations { get; set; }
        public List<Reservation> AccommodationReservations { get; set; }
        private ReservedAccommodationService _reservedAccommodationService;
        private PostponementService _postponementService;


        public Guest1ChangeReservationView(Reservation selectedReservation, User loggedUser, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, PostponementService postponementService)
        {
            InitializeComponent();

            _selectedReservation = selectedReservation;
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            _postponementService = postponementService;
            LoggedUser = loggedUser;

            int minimumDaysOfReservation = _selectedReservation.Accommodation.MinReservationDays;
            MinDaysLabel.Content = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
            int maxGuests = _selectedReservation.Accommodation.MaxGuests;
            MaxGuestsLabel.Content = $"Maximum number of guests: {maxGuests} guests.";

            startDateDp.DisplayDateStart = DateTime.Today.AddDays(1);

            Reservations = _reservationService.GetAll();
            AccommodationReservations = _reservationService.GetAccommodationReservations(_selectedReservation.Accommodation);

            foreach (Reservation reservation in AccommodationReservations)
            {
                if (reservation.getID() == selectedReservation.getID())
                {
                    AccommodationReservations.Remove(reservation);
                    break;
                }
            }

            DisableReservedDates(AccommodationReservations, startDateDp, endDateDp);
            DisableAllImpossibleDates(startDateDp, minimumDaysOfReservation);
        }


        private void Postpone(object sender, RoutedEventArgs e)
        {
            Postponement postponement = new Postponement(_reservationService.GetById(_selectedReservation.getID()), (DateTime)startDateDp.SelectedDate, (DateTime)endDateDp.SelectedDate, PostponementStatus.Pending, false);
            _postponementService.Save(postponement);
            MessageBox.Show("Request sent successfully");
            Close();
        }

        private void DisableReservedDates(List<Reservation> accommodationReservations, DatePicker startDatePicker, DatePicker endDatePicker)
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
        }
    }
}
