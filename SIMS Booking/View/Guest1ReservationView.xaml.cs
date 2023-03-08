using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.View;

public partial class Guest1ReservationView : Window
{
    private readonly Accommodation _selectedAccommodation;
    public User LoggedUser { get; set; }
    private ReservationRepository _reservationRepository;
    public List<Reservation> Reservations { get; set; }
    public List<Reservation> AccommodationReservations { get; set; }

    public Guest1ReservationView(Accommodation selectedAccommodation, User loggedUser, ReservationRepository reservationRepository)
    {
        _selectedAccommodation = selectedAccommodation;
        _reservationRepository = reservationRepository;
        startDateDp.SelectedDate = DateTime.Today;
        endDateDp.SelectedDate = DateTime.Today;
        LoggedUser = loggedUser;
        Reservations = _reservationRepository.Load();
        AccommodationReservations = getAccommodationReservations(Reservations);
        DisableReservedDates(AccommodationReservations, startDateDp, endDateDp);

        InitializeComponent();

        int minimumDaysOfReservation = _selectedAccommodation.MinReservationDays;
        MinDaysLabel.Content = $"Minimum duration of reservation: {minimumDaysOfReservation} days.";
        int maxGuests = _selectedAccommodation.MaxGuests;
        MaxGuestsLabel.Content = $"Maximum number of guests: {maxGuests} guests.";

    }

    private void Reserve(object sender, RoutedEventArgs e)
    {


        if (_selectedAccommodation.MaxGuests < Convert.ToInt32(guestNumberTextBox.Text))
        {
            MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this accommodation ({_selectedAccommodation.MaxGuests} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }



    }

    private void DisableReservedDates(List<Reservation> accommodationReservations, DatePicker startDatePicker, DatePicker endDatePicker)
    {
        foreach (var reservation in accommodationReservations)
        {
            var startDate = reservation.StartDate.Date;
            var endDate = reservation.EndDate.Date;

            var range = new CalendarDateRange(startDate, endDate);
            startDatePicker.BlackoutDates.Add(range);
            endDatePicker.BlackoutDates.Add(range);
        }
    }


    private List<Reservation> getAccommodationReservations(List<Reservation> reservations)
    {
        List<Reservation> accommodationReservations = new List<Reservation>();

        foreach (Reservation reservation in reservations)
        {
            if (reservation.User.ID == LoggedUser.ID)
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
            DateTime? minimumEndDate = startDateDp.SelectedDate.Value.AddDays(_selectedAccommodation.MinReservationDays);
            endDateDp.DisplayDateStart = minimumEndDate;

            if (endDateDp.SelectedDate.HasValue && endDateDp.SelectedDate.Value < minimumEndDate)
            {
                endDateDp.SelectedDate = minimumEndDate;
            }
        }
    }
}