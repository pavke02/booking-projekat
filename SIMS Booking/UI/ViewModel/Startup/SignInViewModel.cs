using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.View;
using SIMS_Booking.UI.ViewModel.Owner;
using SIMS_Booking.Utility.Stores;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.UI.View.Guest1;
using SIMS_Booking.UI.ViewModel.Guest1;

namespace SIMS_Booking.UI.ViewModel.Startup
{
    public partial class SignInViewModel : ViewModelBase
    {
        #region Services
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
        private readonly RenovationAppointmentService _renovationAppointmentService;
        #endregion

        private TourReview _tourReview;
        private Tour _tour;

        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public ICommand NavigateToSignUpCommand { get; }

        #region Property
        private bool _passwordErrorText;
        public bool PasswordErrorText
        {
            get => _passwordErrorText;
            set
            {
                if (value != _passwordErrorText)
                {
                    _passwordErrorText = value;
                    OnPropertyChanged();
                }

            }
        }

        private bool _usernameErrorText;
        public bool UsernameErrorText
        {
            get => _usernameErrorText;
            set
            {
                if (value != _usernameErrorText)
                {
                    _usernameErrorText = value;
                    OnPropertyChanged();
                }

            }
        }

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

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        } 
        #endregion

        public SignInViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            #region ServiceInitializaton
            _userService = new UserService();
            _accommodationService = new AccommodationService();
            _cityCountryCsvRepository = new CityCountryCsvRepository();
            _tourService = new TourService();
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
            _cancellationCsvCrudRepository = new CancellationCsvCrudRepository();
            _textBox = new TextBox();
            _guestReviewService = new GuestReviewService();
            _ownerReviewService = new OwnerReviewService();
            _tourPointService = new TourPointService();
            _confirmTourService = new ConfirmTourService();
            _reservedAccommodationService = new ReservedAccommodationService();
            _userAccommodationService = new UsersAccommodationService();
            _renovationAppointmentService = new RenovationAppointmentService();
            #endregion

            #region LoadingData
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
            _renovationAppointmentService.LoadAccommodationInRenovationAppointment(_accommodationService);
            #endregion

            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;

            NavigateToSignUpCommand = new NavigateCommand(CreateSignUpNavigationService(modalNavigationStore));
        }

        private INavigationService CreateSignUpNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new NavigationService<SignUpViewModel>
                (_navigationStore, () => new SignUpViewModel(_navigationStore, modalNavigationStore, _userService));
        }

        #region SignIn
        [RelayCommand]
        public void SignIn()
        {
            User user = _userService.GetByUsername(Username);     
            
            if (UsernameErrorText = user == null) return;
            if (PasswordErrorText = !_userService.CheckPassword(user, Password)) return;           
            switch (user.Role)
            {
                case Roles.Owner:
                    //Question: da li postoji bolji nacin(ovaj je izuzetno glup, zaobilazi celu strukturu)
                    _navigationStore.CurrentViewModel = new OwnerMainViewModel(_accommodationService, _cityCountryCsvRepository, _reservationService, _guestReviewService,
                        _userAccommodationService, _ownerReviewService, _postponementService, user, _cancellationCsvCrudRepository, _userService, _renovationAppointmentService, _navigationStore, _modalNavigationStore);
                    break;
                case Roles.Guest1:
                    _navigationStore.CurrentViewModel = new Guest1MainViewModel(_accommodationService,
                        _cityCountryCsvRepository,
                        _reservationService, _reservedAccommodationService, user, _postponementService,
                        _cancellationCsvCrudRepository, _ownerReviewService, _renovationAppointmentService,
                        _guestReviewService, _modalNavigationStore);
                    break;
                case Roles.Guest2:
                    Guest2MainView guest2View = new Guest2MainView(_tourService, user, _vehicleService,
                    _guideReviewService, _reservedTourService);
                    guest2View.Show();
                    break;
                case Roles.Driver:
                    DriverView driverView = new DriverView(user, _ridesService, _finishedRidesService, _vehicleService,
                        _driverLanguagesService, _driverLocationsService, _cityCountryCsvRepository);
                    driverView.Show();
                    //CheckFastRides(user);
                    break;
                case Roles.Guide:
                    GuideMainView guideView = new GuideMainView(_tourService, _confirmTourService, _tourPointService,
                        _textBox, _userService, _tourReview, _tour, _tourReviewService);
                    guideView.Show();
                    break;
             }
        }                        
        #endregion
    } 
}
