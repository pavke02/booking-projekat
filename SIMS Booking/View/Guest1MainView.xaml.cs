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

namespace SIMS_Booking.View
{

    public partial class Guest1MainView : Window, IObserver
    {        
        public ObservableCollection<Accommodation> Accommodations { get; set; }        
        public ObservableCollection<Reservation> UserReservations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> AccommodationsReorganized { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationRepository _accommodationRepository;
        private readonly CityCountryRepository _cityCountryRepository;
        private ReservationRepository _reservationRepository;
        private ReservedAccommodationRepository _reservedAccommodationRepository;

        public Guest1MainView(AccommodationRepository accommodationRepository, CityCountryRepository cityCountryRepository, ReservationRepository reservationRepository, ReservedAccommodationRepository reservedAccommodationRepository, User loggedUser)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            LoggedUser = loggedUser;

            _accommodationRepository = accommodationRepository;
            _accommodationRepository.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.GetAll());

            _reservationRepository = reservationRepository;
            _reservationRepository.Subscribe(this);
            UserReservations = new ObservableCollection<Reservation>(_reservationRepository.GetReservationsByUser(loggedUser.getID()));            

            _cityCountryRepository = cityCountryRepository;  
            
            _reservedAccommodationRepository = reservedAccommodationRepository;
        }

        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView(_accommodationRepository, _cityCountryRepository);
            filterView.Show();
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            Guest1ReservationView reservationView =
                new Guest1ReservationView(SelectedAccommodation, LoggedUser, _reservationRepository, _reservedAccommodationRepository);
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
            UpdateAccommodations(_accommodationRepository.GetAll());
            UpdateUserReservations(_reservationRepository.GetReservationsByUser(LoggedUser.getID()).ToList());
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
