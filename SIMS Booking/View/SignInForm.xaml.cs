using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;


namespace SIMS_Booking.View
{
    public partial class SignInForm : Window
    {
        private readonly UserService _userService;
        private readonly AccommodationService _accommodationService;
        private readonly CityCountryRepository _cityCountryRepository;   
        private readonly ReservationService _reservationService;
        private readonly TourRepository _tourRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly GuestReviewService _guestReviewService; 
        private readonly OwnerReviewService _ownerReviewService;
        private readonly PostponementService _postponementService;
        
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly UsersAccommodationService _userAccommodationService;
        private readonly DriverLanguagesRepository _driverLanguagesRepository;
        private readonly DriverLocationsRepository _driverLocationsRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly ConfirmTourRepository _confirmTourRepository;


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
        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            startupWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _userService = new UserService();
            _accommodationService = new AccommodationService();
            _cityCountryRepository = new CityCountryRepository();   
            _reservationService = new ReservationService();
            _postponementService = new PostponementService();
            _tourRepository = new TourRepository(); // sve ture ali nemamo  tourPoint = null
            _vehicleRepository = new VehicleRepository();
            _guestReviewService = new GuestReviewService();
            _ownerReviewService = new OwnerReviewService();
            _tourPointRepository = new TourPointRepository(); // svi tourPointi
            _confirmTourRepository = new ConfirmTourRepository();
            

            _reservedAccommodationService = new ReservedAccommodationService();
            _userAccommodationService = new UsersAccommodationService();

            _reservedAccommodationService.LoadAccommodationsAndUsersInReservation(_userService, _accommodationService, _reservationService);
            _userAccommodationService.LoadUsersInAccommodation(_userService, _accommodationService);
            _guestReviewService.LoadReservationInGuestReview(_reservationService);
            _ownerReviewService.LoadReservationInOwnerReview(_reservationService);
            _postponementService.LoadReservationInPostponement(_reservationService);

            _driverLanguagesRepository = new DriverLanguagesRepository();
            _driverLocationsRepository = new DriverLocationsRepository();

            _driverLanguagesRepository.AddDriverLanguagesToVehicles(_vehicleRepository);
            _driverLocationsRepository.AddDriverLocationsToVehicles(_vehicleRepository);
            //_confirmTourRepository.loadGuests(_userService);
            _tourRepository.LoadCheckpoints(_tourPointRepository);
            //_tourCheckpointRepository.LoadCheckpointsInTour(_tourRepository, _tourPointRepository);
        }

        #region SignIn
        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userService.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {                    
                    switch(user.Role)
                    {
                        case Roles.Owner:
                            OwnerMainView ownerView = new OwnerMainView(_accommodationService, _cityCountryRepository, _reservationService, _guestReviewService, _userAccommodationService, _ownerReviewService, user);
                            ownerView.Show();
                            break;
                        case Roles.Guest1:
                            Guest1MainView guest1View = new Guest1MainView(_accommodationService, _cityCountryRepository, _reservationService, _reservedAccommodationService ,user, _postponementService);
                            guest1View.Show();
                            break;
                        case Roles.Guest2:
                            Guest2MainView guest2View = new Guest2MainView(_tourRepository,user, _vehicleRepository);
                            guest2View.Show();
                            break;
                        case Roles.Driver:
                            DriverView driverView = new DriverView(user, _vehicleRepository, _driverLanguagesRepository, _driverLocationsRepository, _cityCountryRepository);
                            driverView.Show();
                            break;
                        case Roles.Guide:
                            GuideMainView guideView = new GuideMainView(_tourRepository, _confirmTourRepository, _tourPointRepository);
                            guideView.Show();
                            break;
                    }
                    Close();
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
            SignUpView signUpView = new SignUpView(_userService);
            signUpView.Show();
            Close();
        }
    }
}
