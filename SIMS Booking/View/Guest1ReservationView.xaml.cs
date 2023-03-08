using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.View;

public partial class Guest1ReservationView : Window, IObserver
{
    private readonly Accommodation _selectedAccommodation;
    public User LoggedUser { get; set; }
    private ReservationRepository _reservationRepository;
    public List<Reservation> Reservations { get; set; }
    public List<Reservation> AccommodationReservations { get; set; }
    private ReservedAccommodationRepository _reservedAccommodationRepository;


    public Guest1ReservationView(Accommodation selectedAccommodation, User loggedUser, ReservationRepository reservationRepository, ReservedAccommodationRepository reservedAccommodationRepository)
    {
        InitializeComponent();

        _selectedAccommodation = selectedAccommodation;
        _reservationRepository = reservationRepository;
        _reservedAccommodationRepository = reservedAccommodationRepository;
        LoggedUser = loggedUser;

        int minimumDaysOfReservation = _selectedAccommodation.MinReservationDays;
        MinDaysLabel.Content = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
        int maxGuests = _selectedAccommodation.MaxGuests;
        MaxGuestsLabel.Content = $"Maximum number of guests: {maxGuests} guests.";

        startDateDp.DisplayDateStart = DateTime.Today.AddDays(1);

        Reservations = _reservationRepository.GetAll();
        AccommodationReservations = getAccommodationReservations(Reservations);
        DisableReservedDates(AccommodationReservations, startDateDp, endDateDp);
        DisableAllImpossibleDates(startDateDp, minimumDaysOfReservation);
    }


    private void Reserve(object sender, RoutedEventArgs e)
    {

        if (_selectedAccommodation.MaxGuests < Convert.ToInt32(guestNumberTextBox.Text))
        {
            MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this accommodation ({_selectedAccommodation.MaxGuests} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Reservation reservation = new Reservation((DateTime)startDateDp.SelectedDate, (DateTime)endDateDp.SelectedDate,
            _selectedAccommodation, LoggedUser);
        _reservationRepository.Save(reservation);
        ReservedAccommodation reservedAccommodation =
            new ReservedAccommodation(LoggedUser.ID, _selectedAccommodation.ID, reservation.ID);
        _reservedAccommodationRepository.Save(reservedAccommodation);
        Update();
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


    private List<Reservation> getAccommodationReservations(List<Reservation> reservations)
    {
        List<Reservation> accommodationReservations = new List<Reservation>();

        foreach (Reservation reservation in reservations)
        {
            if (reservation.Accommodation.ID == _selectedAccommodation.ID)
            {
                accommodationReservations.Add(reservation);
            }
        }

        return accommodationReservations;
    }

    private void startDateDpSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (startDateDp.SelectedDate.HasValue)
        {
            if (!endDateDp.IsEnabled)
            {
                endDateDp.IsEnabled = true;
            }
            DateTime? minimumEndDate = startDateDp.SelectedDate.Value.AddDays(_selectedAccommodation.MinReservationDays);
            endDateDp.DisplayDateStart = minimumEndDate;

            endDateDp.SelectedDate = minimumEndDate;
            endDateDp.DisplayDateEnd = GetFirstBlackoutDateAfterDate(endDateDp, startDateDp.SelectedDate.Value);
        }
    }

    public DateTime? GetFirstBlackoutDateAfterDate(DatePicker datePicker, DateTime date)
    {
        var blackoutDates = datePicker.BlackoutDates;

        // Find the first blackout date after the specified date
        var nextBlackoutDate = blackoutDates.FirstOrDefault(d => d.Start > date);

        // If no blackout date is found, return null
        if (nextBlackoutDate == null)
        {
            return null;
        }

        return nextBlackoutDate.Start.AddDays(-1);
    }

    private void UpdateReservations(List<Reservation> reservations)
    {
        Reservations.Clear();
        foreach (var accommodation in reservations)
            Reservations.Add(accommodation);
    }

    public void Update()
    {
        UpdateReservations(_reservationRepository.GetAll());
    }
}