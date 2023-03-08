using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.View
{

    public partial class Guest1MainView : Window, IObserver
    {        
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> Reservations { get; set; }
        public ObservableCollection<Reservation> UserReservations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> AccommodationsReorganized { get; set; }
        public User LoggedUser { get; set; }


        private readonly AccomodationRepository _accommodationRepository;
        private readonly CityCountryRepository _cityCountryRepository;
        private ReservationRepository _reservationRepository;

        public Guest1MainView(AccomodationRepository accommodationRepository, CityCountryRepository cityCountryRepository, ReservationRepository reservationRepository, User loggedUser)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            LoggedUser = loggedUser;

            _accommodationRepository = accommodationRepository;
            _cityCountryRepository = cityCountryRepository;
            _reservationRepository = reservationRepository;
            _accommodationRepository.Subscribe(this);

            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.Load());
            Reservations = new ObservableCollection<Reservation>(reservationRepository.Load());
            UserReservations = getUserReservations(Reservations);
        }

        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView(_accommodationRepository, _cityCountryRepository);
            filterView.Show();
        }

        private ObservableCollection<Reservation> getUserReservations(ObservableCollection<Reservation> reservations)
        {
            ObservableCollection<Reservation> userReservations =
                new ObservableCollection<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.User.ID == LoggedUser.ID)
                {
                    userReservations.Add(reservation);
                }
            }

            return userReservations;
        }

        public void UpdateFilters(ObservableCollection<Accommodation> accommodations)
        {
            DataGridAccommodations.ItemsSource = accommodations;
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            Guest1ReservationView reservationView =
                new Guest1ReservationView(SelectedAccommodation, LoggedUser, _reservationRepository);
            reservationView.Show();
        }

        private void OpenGallery(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationRepository.GetAll());
        }
    }
}
