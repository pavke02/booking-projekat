using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

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

        public ReservationOfVehicle SelectedVehicleReservation { get; set; }
        

        public ObservableCollection<TourReservation> TourReservation { get; set; }
        public ObservableCollection<ReservationOfVehicle> ReservationOfVehicle { get; set; }


        private readonly TourService _tourService;
        private readonly VehicleService _vehicleService;
        private readonly ReservedTourService _reservedTourService;
        private readonly GuideReviewService _guideReviewService;
        private readonly VoucherService _voucherService;
        private readonly TourReservation tourReservation;
        private readonly VehicleReservationService _vehicleReservationService;
        private readonly DriverLocationsService _driverLocationsService;
        private readonly TourRequestService _tourRequestService;
        private readonly GroupRideService _groupRideService;
       

        /*VehicleCsvCrudRepository vehicleCsvCrudRepository*/
        public Guest2MainView(TourService tourService, User loggedUser,VehicleService vehicleService, GuideReviewService guideReviewService, ReservedTourService reservedTourService, DriverLocationsService driverLocationsService, VehicleReservationService vehicleReservationService, VoucherService voucherService, GroupRideService groupRideService, TourRequestService tourRequestService)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourService = tourService;
            _tourService.Subscribe(this);

            _groupRideService = groupRideService;
            _vehicleService = vehicleService;
            _vehicleService.Subscribe(this);

         
            _tourRequestService = tourRequestService;
            _guideReviewService = guideReviewService;
            _driverLocationsService = driverLocationsService;
            _vehicleReservationService = vehicleReservationService;
            _voucherService = voucherService;

            _reservedTourService = new ReservedTourService(new CsvCrudRepository<TourReservation>());
            Tours = new ObservableCollection<Tour>(_tourService.GetAll());

            TourReservation = new ObservableCollection<TourReservation>(_reservedTourService.GetAll());
            ReservationOfVehicle = new ObservableCollection<ReservationOfVehicle>(_vehicleReservationService.GetAll());
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

            return SelectedTour.GetId() + 1;
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
                new Guest2DrivingReservationView(SelectedVehicle, LoggedUser,_vehicleReservationService, _vehicleService, _driverLocationsService);

            reservationView.Show();

        }

        private void ShowReserved_Tours(object sender, RoutedEventArgs e)
        {
            Guest2ReservedTours reservationView =
                new Guest2ReservedTours(LoggedUser, _guideReviewService, _reservedTourService);

            reservationView.Show();

        }

        private void Group_Ride(object sender, RoutedEventArgs e)
        {
            Guest2GroupRide ride =
                new Guest2GroupRide(LoggedUser, _groupRideService);

            ride.Show();

        }


        private void Tour_Requests(object sender, RoutedEventArgs e)
        {
            Guest2TourRequestAllDisplays requests =
                new Guest2TourRequestAllDisplays(LoggedUser, _tourRequestService);

            requests.Show();

        }

        private void Reserve_Tour(object sender, RoutedEventArgs e)
        {

            if (SelectedTour != null)
            {
                
                Guest2TourReservation reservation = new Guest2TourReservation(SelectedTour, LoggedUser, _reservedTourService, _voucherService, SelectedVoucher);
                reservation.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select the tour!");
            }

        }

        private void QuickTaxi_Click(object sender, RoutedEventArgs e)
        {
            /*
            Guest2FindingTaxiFast guest2FindingTaxiFast = new Guest2FindingTaxiFast(LoggedUser, _vehicleReservationService, _driverLocationsService);
            guest2FindingTaxiFast.Show();
            */

        }
    }
}

