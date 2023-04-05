using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.View
{

    public partial class Guest1MainView : Window, IObserver
    {        
        public ObservableCollection<Accommodation> Accommodations { get; set; }        
        public ObservableCollection<Reservation> UserReservations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Reservation SelectedReservation { get; set; }
        public ObservableCollection<Accommodation> AccommodationsReorganized { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly CityCountryRepository _cityCountryRepository;
        private ReservationService _reservationService;
        private ReservedAccommodationService _reservedAccommodationService;
        private PostponementService _postponementService;

        public Guest1MainView(AccommodationService accommodationService, CityCountryRepository cityCountryRepository, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, User loggedUser, PostponementService postponementService)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            LoggedUser = loggedUser;

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetAll());

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            UserReservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsByUser(loggedUser.getID()));

            _postponementService = postponementService;

            _cityCountryRepository = cityCountryRepository;

            _reservedAccommodationService = reservedAccommodationService;
        }

        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView(_accommodationService, _cityCountryRepository);
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

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetAll());
            UpdateUserReservations(_reservationService.GetReservationsByUser(LoggedUser.getID()).ToList());
        }

        public void CancelReservation(object sender, RoutedEventArgs e)
        {
            List<Reservation> newReservations = _reservationService.GetAll();
            foreach (Reservation reservation in newReservations)
            {
                if (reservation.getID() == SelectedReservation.getID())
                {
                    newReservations.Remove(reservation);
                    UpdateUserReservations(newReservations);
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

       
    }
}
