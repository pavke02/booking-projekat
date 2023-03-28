using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;


namespace SIMS_Booking.View
{
    public partial class SignInForm : Window
    {
        private readonly UserRepository _userRepository;
        private readonly AccomodationRepository _accommodationRepository;
        private readonly CityCountryRepository _cityCountryRepository;   
        private readonly ReservationRepository _reservationRepository;
        private readonly TourRepository _tourRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly GuestReviewRepository _guestReviewRepository; 
        
        private readonly ReservedAccommodationRepository _reservedAccommodationRepository;
        private readonly UsersAccommodationRepository _usersAccommodationRepository;
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

            _userRepository = new UserRepository();
            _accommodationRepository = new AccomodationRepository();
            _cityCountryRepository = new CityCountryRepository();   
            _reservationRepository = new ReservationRepository();            
            _tourRepository = new TourRepository(); // sve ture ali nemamo  tourPoint = null
            _vehicleRepository = new VehicleRepository();
            _guestReviewRepository = new GuestReviewRepository();                
            _tourPointRepository = new TourPointRepository(); // svi tourPointi
            _confirmTourRepository = new ConfirmTourRepository();

            _reservedAccommodationRepository = new ReservedAccommodationRepository();
            _usersAccommodationRepository = new UsersAccommodationRepository();

            _reservedAccommodationRepository.LoadAccommodationsAndUsersInReservation(_userRepository, _accommodationRepository, _reservationRepository);
            _usersAccommodationRepository.LoadUsersInAccommodation(_userRepository, _accommodationRepository);
            _guestReviewRepository.LoadReservationInGuestReview(_reservationRepository);

            _driverLanguagesRepository = new DriverLanguagesRepository();
            _driverLocationsRepository = new DriverLocationsRepository();

            _driverLanguagesRepository.AddDriverLanguagesToVehicles(_vehicleRepository);
            _driverLocationsRepository.AddDriverLocationsToVehicles(_vehicleRepository);
            _confirmTourRepository.loadGuests(_userRepository);
            _tourRepository.LoadCheckpoints(_tourPointRepository);
            //_tourCheckpointRepository.LoadCheckpointsInTour(_tourRepository, _tourPointRepository);
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = _userRepository.GetByUsername(Username);
            if (user != null)
            {
                if (user.Password == txtPassword.Password)
                {                    
                    switch(user.Role)
                    {
                        case Roles.Owner:

                            OwnerMainView ownerView = new OwnerMainView(_accommodationRepository, _cityCountryRepository, _reservationRepository, _guestReviewRepository, _reservedAccommodationRepository, _usersAccommodationRepository, user);
                            ownerView.Show();
                            break;
                        case Roles.Guest1:
                            Guest1MainView guest1View = new Guest1MainView(_accommodationRepository, _cityCountryRepository, _reservationRepository, _reservedAccommodationRepository ,user);
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

        private void SignUp(object sender, RoutedEventArgs e)
        {
            SignUpView signUpView = new SignUpView(_userRepository);
            signUpView.Show();
            Close();
        }
    }
}
