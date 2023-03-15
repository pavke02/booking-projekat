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
    /// <summary>
    /// Interaction logic for Guest2MainView.xaml
    /// </summary>
    public partial class Guest2MainView : Window, IObserver
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Accommodation SelectedTour { get; set; }
        public User LoggedUser { get; set; }

        private readonly TourRepository _tourRepository;



        public Guest2MainView(TourRepository tourRepository, User loggedUser)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourRepository = tourRepository;
            _tourRepository.Subscribe(this);
            Tours = new ObservableCollection<Tour>(tourRepository.GetAll());

        
        }
        private void UpdateTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (var tour in tours)
                Tours.Add(tour);
        }

        public void Update()
        {
            UpdateTours(_tourRepository.GetAll());

        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            /*Guest2ReservationView reservationView =
                new Guest2ReservationView(SelectedAccommodation, LoggedUser, _reservationRepository, _reservedAccommodationRepository);
            
            reservationView.Show();
            */
        }

    }
}
