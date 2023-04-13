using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.View.Owner;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        private readonly UserService _userService;
        private readonly AccommodationService _accommodationService;
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;
        private readonly ReservationService _reservationService;
        private readonly TourService _tourService;
        private readonly ReservedTourService _reservedTourService;
        private readonly TourReviewService _tourReviewService;
        private readonly VehicleService _vehicleService;
        private readonly TextBox _textBox;

        private readonly DriverLanguagesService _driverLanguagesService;
        private readonly DriverLocationsService _driverLocationsService;
        private readonly RidesService _ridesService;
        private readonly FinishedRidesService _finishedRidesService;
        private readonly GuestReviewService _guestReviewService;
        private readonly OwnerReviewService _ownerReviewService;
        private readonly GuideReviewService _guideReviewService;
        private readonly PostponementService _postponementService;
        private readonly CancellationCsvCrudRepository _cancellationCsvCrudRepository;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly UsersAccommodationService _userAccommodationService;
        private readonly TourPointService _tourPointService;
        private readonly ConfirmTourService _confirmTourService;

        private TourReview _tourReview;
        private Tour _tour;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public SignInView()
        {
            InitializeComponent();
            //DataContext = this;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //startupWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _userService = new UserService();
            _accommodationService = new AccommodationService();
            _cityCountryCsvRepository = new CityCountryCsvRepository();
            _tourService = new TourService(); // sve ture ali nemamo  tourPoint = null

            _reservedTourService = new ReservedTourService();


            _tourReviewService = new TourReviewService();

            _reservationService = new ReservationService();
            _postponementService = new PostponementService();
            _vehicleService = new VehicleService();
            _driverLanguagesService = new DriverLanguagesService();
            _driverLocationsService = new DriverLocationsService();
            _ridesService = new RidesService();
            _finishedRidesService = new FinishedRidesService();
            _guestReviewService = new GuestReviewService();
            _ownerReviewService = new OwnerReviewService();

            _guideReviewService = new GuideReviewService();
            //_tourPointCsvCrudRepository = new TourPointCsvCrudRepository(); // svi tourPointi
            //_confirmTourCsvCrudRepository = new ConfirmTourCsvCrudRepository();
            _cancellationCsvCrudRepository = new CancellationCsvCrudRepository();
            _textBox = new TextBox();
            _guestReviewService = new GuestReviewService();
            _ownerReviewService = new OwnerReviewService();
            _tourPointService = new TourPointService(); // svi tourPointi
            _confirmTourService = new ConfirmTourService();
            _reservedAccommodationService = new ReservedAccommodationService();
            _userAccommodationService = new UsersAccommodationService();

            _reservedAccommodationService.LoadAccommodationsAndUsersInReservation(_userService, _accommodationService, _reservationService);
            _userAccommodationService.LoadUsersInAccommodation(_userService, _accommodationService);
            _guestReviewService.LoadReservationInGuestReview(_reservationService);
            _ownerReviewService.LoadReservationInOwnerReview(_reservationService);
            _postponementService.LoadReservationInPostponement(_reservationService);
            _driverLanguagesService.AddDriverLanguagesToVehicles(_vehicleService);
            _driverLocationsService.AddDriverLocationsToVehicles(_vehicleService);
            _confirmTourService.loadGuests(_userService);
            _tourService.LoadCheckpoints(_tourPointService);
            _tourReviewService.loadusers(_userService, _confirmTourService);
            _tourReviewService.loadCheckPoints(_confirmTourService, _tourReviewService);
        }

        #region SignIn
        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userService.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {
                    switch (user.Role)
                    {
                        case Roles.Owner:
                            OwnerMainView ownerView = new OwnerMainView(_accommodationService, _cityCountryCsvRepository, _reservationService, _guestReviewService, _userAccommodationService, _ownerReviewService, _postponementService, user, _cancellationCsvCrudRepository, _userService);
                            ownerView.Show();
                            break;
                        case Roles.Guest1:
                            Guest1MainView guest1View = new Guest1MainView(_accommodationService, _cityCountryCsvRepository, _reservationService, _reservedAccommodationService, user, _postponementService, _cancellationCsvCrudRepository, _ownerReviewService);
                            guest1View.Show();
                            break;
                        case Roles.Guest2:
                            Guest2MainView guest2View = new Guest2MainView(_tourService, user, _vehicleService, _guideReviewService, _reservedTourService);
                            guest2View.Show();
                            break;
                        case Roles.Driver:
                            DriverView driverView = new DriverView(user, _ridesService, _finishedRidesService, _vehicleService, _driverLanguagesService, _driverLocationsService, _cityCountryCsvRepository);
                            driverView.Show();
                            CheckFastRides(user);
                            break;
                        case Roles.Guide:
                            GuideMainView guideView = new GuideMainView(_tourService, _confirmTourService, _tourPointService, _textBox, _userService, _tourReview, _tour, _tourReviewService);
                            guideView.Show();
                            break;
                    }
                    //Close();
                }
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
        #endregion

        private void SignUp(object sender, RoutedEventArgs e)
        {
            //SignUpView signUpView = new SignUpView(_userService);
            //signUpView.Show();
            //Close();
        }

        public void CheckFastRides(User user)
        {
            foreach (Rides ride in _ridesService.GetAll())
            {
                if (ride.DriverID == user.getID() && ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now && ride.Fast == true)
                {
                    MessageBox.Show("You have new fast ride(s)!");
                    break;
                }
            }
        }
    }
}
