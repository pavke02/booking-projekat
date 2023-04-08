using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Service;

namespace SIMS_Booking.View
{
    public partial class Guest2MainView : Window, IObserver
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public Vehicle SelectedVehicle { get; set; }
        public User LoggedUser { get; set; }
        public int searchGuestNumber;
        public DriverLocations driverLocations { get; set; }

        public ObservableCollection<TourReservation> TourReservation { get; set; }


        private readonly ReservedToursCsvCrudRepository _reservedToursCsvCrudRepository;
        private readonly TourService _tourService;
        private readonly VehicleCsvCrudRepository _vehicleCsvCrudRepository;
        private readonly VehicleReservationCsvCrudRepository _vehicleReservationCsvCrudRepository;
        private readonly DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRepository;
        private readonly TourReservation tourReservation;

        public Guest2MainView(TourService tourService, User loggedUser, VehicleCsvCrudRepository vehicleCsvCrudRepository)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourService = tourService;
            _tourService.Subscribe(this);
            _vehicleCsvCrudRepository = vehicleCsvCrudRepository;
            _vehicleCsvCrudRepository.Subscribe(this);

            _reservedToursCsvCrudRepository = new ReservedToursCsvCrudRepository();

            Tours = new ObservableCollection<Tour>(_tourService.GetAll());
            TourReservation = new ObservableCollection<TourReservation>(_reservedToursCsvCrudRepository.GetAll());
        }
        private void UpdateTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (var tour in tours)
                Tours.Add(tour);
        }

        public void Update()
        {
            UpdateTours(_tourService.GetAll());
        }

        public int NextId()
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetAll());

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

