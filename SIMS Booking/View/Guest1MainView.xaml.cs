using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility;

namespace SIMS_Booking.View
{

    public partial class Guest1MainView : Window, IObserver
    {

        public ObservableCollection<Accommodation> Accommodations { get; set; }        
        public ObservableCollection<Reservation> UserReservations { get; set; }
        public ObservableCollection<Postponement> UserPostponements { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Reservation SelectedReservation { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;
        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly PostponementService _postponementService;
        private readonly CancellationCsvCrudRepository _cancellationCsvCrudRepository;
        private readonly OwnerReviewService _ownerReviewService;

        public Guest1MainView(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, User loggedUser, PostponementService postponementService, CancellationCsvCrudRepository cancellationCsvCrudRepository, OwnerReviewService ownerReviewService)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            LoggedUser = loggedUser;

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.SortBySuperOwner(_accommodationService.GetAll()));

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            UserReservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsByUser(loggedUser.getID()));

            _postponementService = postponementService;
            NotificationTimer timer = new NotificationTimer(LoggedUser, _postponementService);
            _postponementService.Subscribe(this);
            UserPostponements = new ObservableCollection<Postponement>(_postponementService.GetPostponementsByUser(loggedUser.getID()));

            _ownerReviewService = ownerReviewService;

            _cityCountryCsvRepository = cityCountryCsvRepository;
            _cancellationCsvCrudRepository = cancellationCsvCrudRepository;

            _reservedAccommodationService = reservedAccommodationService;

        }

        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView(_accommodationService, _cityCountryCsvRepository);
            filterView.Show();
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            Guest1ReservationView reservationView =
                new Guest1ReservationView(SelectedAccommodation, LoggedUser, _reservationService, _reservedAccommodationService);
            reservationView.Show();
        }

        private void OpenGallery(object sender, RoutedEventArgs e)
        {
            Guest1GalleryView galleryView = new Guest1GalleryView(SelectedAccommodation);
            galleryView.Show();
        }

        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        private void UpdateUserReservations(List<Reservation> reservations)
        {
            UserReservations.Clear();
            foreach (var reservation in reservations)
                UserReservations.Add(reservation);
        }

        private void UpdateUserPostponements(List<Postponement> postponements)
        {
           UserPostponements.Clear();
           foreach (var postponement in postponements)
           {
               UserPostponements.Add(postponement);
           }
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetAll());
            UpdateUserReservations(_reservationService.GetReservationsByUser(LoggedUser.getID()).ToList());
            UpdateUserPostponements(_postponementService.GetPostponementsByUser(LoggedUser.getID()).ToList());
        }

        public void CancelReservation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.StartDate - DateTime.Today <
                TimeSpan.FromDays(SelectedReservation.Accommodation.CancellationPeriod))
            {
                MessageBox.Show("It is not possible to cancel reservation after cancellation period.");
                return;
            }
            List<Reservation> newReservations = _reservationService.GetAll();
            foreach (Reservation reservation in newReservations)
            {
                if (reservation.getID() == SelectedReservation.getID())
                {
                    _postponementService.DeletePostponementsByReservationId(reservation.getID());
                    _reservationService.DeleteCancelledReservation(reservation.getID());
                    _cancellationCsvCrudRepository.Save(reservation);
                    newReservations.Remove(reservation);
                    UpdateUserReservations(newReservations);
                    UpdateUserPostponements(_postponementService.GetAll().ToList());
                    return;
                }
            }
        }

        private void ChangeReservation(object sender, RoutedEventArgs e)
        {
            Guest1ChangeReservationView guest1ChangeReservationView =
                new Guest1ChangeReservationView(SelectedReservation, LoggedUser, _reservationService,
                    _reservedAccommodationService, _postponementService);
            guest1ChangeReservationView.Show();
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabC.SelectedIndex == 1)
            {
                ReserveButton.IsEnabled = false;
                ViewGalleryButton.IsEnabled = false;
                AddFiltersButton.IsEnabled = false;
            }
            else if(TabC.SelectedIndex == 0 && DataGridAccommodations.SelectedIndex == -1)
            {
                ReserveButton.IsEnabled = false;
                ViewGalleryButton.IsEnabled = false;
                AddFiltersButton.IsEnabled = true;
            }
            else
            {
                ReserveButton.IsEnabled = true;
                ViewGalleryButton.IsEnabled = true;
                AddFiltersButton.IsEnabled = true;
            }
        }


        private void ReviewReservation(object sender, RoutedEventArgs e)
        {
            Guest1OwnerReviewView reviewView = new Guest1OwnerReviewView(_ownerReviewService, _reservationService, SelectedReservation);
            reviewView.Show();
        }

        private void DisableReview(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation == null) return;

            if (DateTime.Today - SelectedReservation.EndDate > TimeSpan.FromDays(5) || DateTime.Today - SelectedReservation.EndDate < TimeSpan.FromDays(0))
            {
                ReviewButton.IsEnabled = false;
            }
            else
            {
                ReviewButton.IsEnabled = true;
            }
        }
    }
}
