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
using SIMS_Booking.View.Owner;


namespace SIMS_Booking.View
{
    public partial class SignInForm : Window
    {
        private readonly UserService _userService;
        private readonly AccommodationService _accommodationService;
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;   

        private readonly ReservationService _reservationService;
        private readonly TourService _tourService;



        private readonly VehicleService _vehicleService;
        private readonly RidesRepository _ridesRepository;
        private readonly FinishedRidesRepository _finishedRidesRepository;
        private readonly VehicleCsvCrudRepository _vehicleCsvCrudRepository;

        private readonly GuestReviewService _guestReviewService; 
        private readonly OwnerReviewService _ownerReviewService;
        private readonly PostponementService _postponementService;
        private readonly CancellationCsvCrudRepository _cancellationCsvCrudRepository;
        
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly UsersAccommodationService _userAccommodationService;
        private readonly DriverLanguagesCsvCrudRepository _driverLanguagesCsvCrudRepository;
        private readonly DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRepository;
        private readonly TourPointCsvCrudRepository _tourPointCsvCrudRepository;
        private readonly ConfirmTourCsvCrudRepository _confirmTourCsvCrudRepository;


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
            _cityCountryCsvRepository = new CityCountryCsvRepository();   

            _tourService = new TourService(); // sve ture ali nemamo  tourPoint = null

            _reservationService = new ReservationService();
            _postponementService = new PostponementService();

            _vehicleService = new VehicleService();
            _vehicleCsvCrudRepository = new VehicleCsvCrudRepository();
            _ridesRepository = new RidesRepository();
            _finishedRidesRepository = new FinishedRidesRepository();

            _guestReviewService = new GuestReviewService();
            _ownerReviewService = new OwnerReviewService();
            _tourPointCsvCrudRepository = new TourPointCsvCrudRepository(); // svi tourPointi
            _confirmTourCsvCrudRepository = new ConfirmTourCsvCrudRepository();
            _cancellationCsvCrudRepository = new CancellationCsvCrudRepository();

            _reservedAccommodationService = new ReservedAccommodationService();
            _userAccommodationService = new UsersAccommodationService();

            _reservedAccommodationService.LoadAccommodationsAndUsersInReservation(_userService, _accommodationService, _reservationService);
            _userAccommodationService.LoadUsersInAccommodation(_userService, _accommodationService);
            _guestReviewService.LoadReservationInGuestReview(_reservationService);
            _ownerReviewService.LoadReservationInOwnerReview(_reservationService);
            _postponementService.LoadReservationInPostponement(_reservationService);

            _driverLanguagesCsvCrudRepository = new DriverLanguagesCsvCrudRepository();
            _driverLocationsCsvCrudRepository = new DriverLocationsCsvCrudRepository();

            _driverLanguagesCsvCrudRepository.AddDriverLanguagesToVehicles(_vehicleCsvCrudRepository);
            _driverLocationsCsvCrudRepository.AddDriverLocationsToVehicles(_vehicleCsvCrudRepository);
            //_confirmTourRepository.loadGuests(_userService);
            _tourService.LoadCheckpoints(_tourPointCsvCrudRepository);
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
                            OwnerMainView ownerView = new OwnerMainView(_accommodationService, _cityCountryCsvRepository, _reservationService, _guestReviewService, _userAccommodationService, _ownerReviewService, _postponementService, user, _cancellationCsvCrudRepository, _userService);
                            ownerView.Show();
                            break;
                        case Roles.Guest1:
                            Guest1MainView guest1View = new Guest1MainView(_accommodationService, _cityCountryCsvRepository, _reservationService, _reservedAccommodationService ,user, _postponementService, _cancellationCsvCrudRepository, _ownerReviewService);
                            guest1View.Show();
                            break;
                        case Roles.Guest2:
                            Guest2MainView guest2View = new Guest2MainView(_tourService, user, _vehicleCsvCrudRepository);
                            guest2View.Show();
                            break;
                        case Roles.Driver:
                            DriverView driverView = new DriverView(user, _ridesRepository, _finishedRidesRepository, _vehicleCsvCrudRepository, _vehicleService, _driverLanguagesCsvCrudRepository, _driverLocationsCsvCrudRepository, _cityCountryCsvRepository);
                            driverView.Show();
                            break;
                        case Roles.Guide:
                            GuideMainView guideView = new GuideMainView(_tourService, _confirmTourCsvCrudRepository, _tourPointCsvCrudRepository);
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
