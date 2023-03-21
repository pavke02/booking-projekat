using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
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
        public Tour SelectedTour { get; set; }
        public Vehicle SelectedVehicle { get; set; }
        public User LoggedUser { get; set; }
        public int searchGuestNumber;
        public DriverLocations driverLocations { get; set; }

        public ObservableCollection<TourReservation> TourReservation { get; set; }


        private readonly ReservedToursRepository _reservedToursRepository;
        private readonly TourRepository _tourRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly VehicleReservationRepository _vehicleReservationRepository;
        private readonly DriverLocationsRepository _driverLocationsRepository;
        private readonly TourReservation tourReservation;

        public Guest2MainView(TourRepository tourRepository, User loggedUser, VehicleRepository vehicleRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourRepository = tourRepository;
            _tourRepository.Subscribe(this);
            _vehicleRepository = vehicleRepository;
            _vehicleRepository.Subscribe(this);

            _reservedToursRepository = new ReservedToursRepository();

            Tours = new ObservableCollection<Tour>(tourRepository.GetAll());
            TourReservation = new ObservableCollection<TourReservation>(_reservedToursRepository.GetAll());
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

        public int NextId()
        {
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());

            if (Tours.Count < 1)
            {
                return 1;
            }

            return SelectedTour.getID() + 1;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reserve_Taxi(object sender, RoutedEventArgs e)
        {
            Guest2DrivingReservationView reservationView =
                new Guest2DrivingReservationView(SelectedVehicle, LoggedUser);

            reservationView.Show();

        }

        private void Reserve_Tour(object sender, RoutedEventArgs e)
        {

            if (SelectedTour != null)
            {
                //SelectedTour.Name,SelectedTour.Location,SelectedTour.Description,SelectedTour.Language, SelectedTour.MaxGuests,SelectedTour.Time, LoggedUser
                Guest2TourReservation reservation = new Guest2TourReservation(SelectedTour, LoggedUser);
                reservation.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select the tour!");
            }

        }
    }
}

