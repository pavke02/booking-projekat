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
using SIMS_Booking.UI.ViewModel.Driver;
using SIMS_Booking.UI.ViewModel.Guest1;
using SIMS_Booking.UI.ViewModel.Guide;
using SIMS_Booking.UI.ViewModel.Owner;
using SIMS_Booking.Utility.Stores;
using System.Windows.Controls;
using System.Windows.Input;

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
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly UsersAccommodationService _userAccommodationService;
        private readonly TourPointService _tourPointService;
        private readonly ConfirmTourService _confirmTourService;
        private readonly VehicleReservationService _vehicleReservationService;
        private readonly VoucherService _voucherService;
        private readonly RenovationAppointmentService _renovationAppointmentService;
        private readonly TourRequestService _tourRequestService;
        private readonly GroupRideService _groupRideService;
        private readonly CommentService _commentService;
        private readonly ForumService _forumService;
        #endregion

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

        public SignInViewModel(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, AccommodationService accommodationService, 
                               CityCountryCsvRepository cityCountryCsvRepository, ReservationService reservationService, GuestReviewService guestReviewService, 
                               UsersAccommodationService usersAccommodationService, OwnerReviewService ownerReviewService, PostponementService postponementService, 
                               UserService userService, RenovationAppointmentService renovationAppointmentService, TourService tourService, ConfirmTourService confirmTourService, 
                               TourPointService tourPointService, TextBox textBox, TourReviewService tourReviewService, RidesService ridesService, 
                               FinishedRidesService finishedRidesService, VehicleService vehicleService, DriverLanguagesService driverLanguagesService, 
                               DriverLocationsService driverLocationsService, ReservedAccommodationService reservedAccommodationService, ReservedTourService reservedTourService, 
                               GuideReviewService guideReviewService, VehicleReservationService vehicleReservationService, VoucherService voucherService,
                               TourRequestService tourRequestService,GroupRideService groupRideService, CommentService commentService, ForumService forumService)

        {
            #region ServiceInitializaton
            _cityCountryCsvRepository = cityCountryCsvRepository;
            _reservationService = reservationService;
            _guestReviewService = guestReviewService;
            _userService = userService;
            _tourService = tourService;
            _userAccommodationService = usersAccommodationService;
            _ownerReviewService = ownerReviewService;
            _accommodationService = accommodationService;
            _postponementService = postponementService;
            _renovationAppointmentService = renovationAppointmentService;
            _confirmTourService = confirmTourService;
            _tourPointService = tourPointService;
            _vehicleService = vehicleService;
            _driverLanguagesService = driverLanguagesService;
            _tourReviewService = tourReviewService;
            _ridesService = ridesService;
            _finishedRidesService = finishedRidesService;
            _driverLocationsService = driverLocationsService;
            _reservedAccommodationService = reservedAccommodationService;
            _reservedTourService = reservedTourService;
            _guideReviewService = guideReviewService;
            _textBox = textBox;
            _vehicleReservationService = vehicleReservationService;
            _voucherService = voucherService;
            _groupRideService = groupRideService;
            _tourRequestService = tourRequestService;
            _commentService = commentService;
            _forumService = forumService;
            #endregion

            #region LoadingData
            _userAccommodationService.LoadUsersInAccommodation(_userService, _accommodationService);
            _reservedAccommodationService.LoadAccommodationsAndUsersInReservation(_userService, _accommodationService, _reservationService);
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
            _commentService.LoadCommenterInComment(_userService);
            _forumService.LoadForumCreatorAndCommentsInForum(_userService, _commentService);
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
                    //ToDo:pretvoriti u komandu(klasu)
                    _navigationStore.CurrentViewModel = new OwnerMainViewModel(_accommodationService, _cityCountryCsvRepository, _reservationService, _guestReviewService,
                      _userAccommodationService, _ownerReviewService, _postponementService, user,
                      _userService, _renovationAppointmentService, _forumService, _commentService, _modalNavigationStore);
                    break;
                case Roles.Guest1:
                    _navigationStore.CurrentViewModel = new Guest1MainViewModel(_accommodationService,
                        _cityCountryCsvRepository,
                        _reservationService, _reservedAccommodationService, user, _postponementService, _ownerReviewService, _renovationAppointmentService,
                        _guestReviewService, _modalNavigationStore);
                    break;
                case Roles.Guest2:
                    Guest2MainView guest2View = new Guest2MainView(_tourService, user, _vehicleService,
                        _guideReviewService, _reservedTourService, _driverLocationsService, _vehicleReservationService, _voucherService, _groupRideService, _tourRequestService);
                    guest2View.Show();
                    break;
                case Roles.Driver:
                    _navigationStore.CurrentViewModel = new DriverViewModel(user, _ridesService, _finishedRidesService, _vehicleService,
                        _driverLanguagesService, _driverLocationsService, _cityCountryCsvRepository, _navigationStore, _modalNavigationStore);
                    break;
                case Roles.Guide:
                    _navigationStore.CurrentViewModel = new MainWindowViewModel(_tourService, _confirmTourService, _tourPointService,
                        _textBox, _userService,  _tourReviewService, _navigationStore, _modalNavigationStore, _tourRequestService);
                    break;
            }
        }
        #endregion
    }
}
