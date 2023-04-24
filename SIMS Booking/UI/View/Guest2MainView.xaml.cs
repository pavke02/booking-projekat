using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.View;

namespace SIMS_Booking.UI.View
{
    public partial class Guest2MainView : Window, IObserver
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public Vehicle SelectedVehicle { get; set; }
        public Voucher SelectedVoucher { get; set; }
        public User LoggedUser { get; set; }
        public int searchGuestNumber;
        public DriverLocations driverLocations { get; set; }
        public TourReservation SelectedTourReservation { get; set; }

        public ObservableCollection<TourReservation> TourReservation { get; set; }

        private readonly TourService _tourService;
        private readonly VehicleService _vehicleService;
        private readonly ReservedTourService _reservedTourService;
        private readonly GuideReviewService _guideReviewService;
        private readonly VoucherService _voucherService;
        private readonly VehicleCsvCrudRepository _vehicleCsvCrudRepository;
        private readonly VehicleReservationCsvCrudRepository _vehicleReservationCsvCrudRepository;
        private readonly DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRepository;
        private readonly TourReservation tourReservation;

        /*VehicleCsvCrudRepository vehicleCsvCrudRepository*/
        public Guest2MainView(TourService tourService, User loggedUser,VehicleService vehicleService, GuideReviewService guideReviewService, ReservedTourService reservedTourService)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourService = tourService;
            _tourService.Subscribe(this);
            

            _vehicleService = vehicleService;
            _vehicleService.Subscribe(this);


            
            _reservedTourService = new ReservedTourService();


            Tours = new ObservableCollection<Tour>(_tourService.GetAll());

            TourReservation = new ObservableCollection<TourReservation>(_reservedTourService.GetAll());

            _guideReviewService = guideReviewService;
            _reservedTourService = reservedTourService;



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

        private void Review_Tour(object sender, RoutedEventArgs e)
        {
            Guest2GuideReviewView reviewView =
                new Guest2GuideReviewView(_guideReviewService,_reservedTourService,SelectedTourReservation);

            reviewView.Show();
          
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
                
                Guest2TourReservation reservation = new Guest2TourReservation(SelectedTour, LoggedUser);
                reservation.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select the tour!");
            }

        }

        private void QuickTaxi_Click(object sender, RoutedEventArgs e)
        {

            Guest2FindingTaxiFast guest2FindingTaxiFast = new Guest2FindingTaxiFast(LoggedUser);
            guest2FindingTaxiFast.Show();


        }
    }
}

