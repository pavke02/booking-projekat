using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class OwnerMainViewModel : ViewModelBase, IObserver, IDataErrorInfo
    {
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly GuestReviewService _guestReviewService;
        private readonly UsersAccommodationService _usersAccommodationService;
        private readonly OwnerReviewService _ownerReviewService;
        private readonly PostponementService _postponementService;
        private readonly UserService _userService;

        private readonly User _user;

        public ICommand PublishCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand ClearURLsCommand { get; }
        public ICommand NavigateToGuestReviewCommand { get; }
        public ICommand NavigateToGuestReviewDetailsCommand { get; }
        public ICommand NavigateToOwnerReviewDetailsCommand { get; }
        public ICommand NavigateToPostponeRequestsCommand { get; }

        #region Property                
        public Dictionary<string, List<string>> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public List<string> TypesCollection { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        public ObservableCollection<GuestReview> PastReservations { get; set; }

        private GuestReview _selectedReview;
        public GuestReview SelectedReview
        {
            get => _selectedReview;
            set
            {
                if (value != _selectedReview)
                {
                    _selectedReview = value;
                    OnPropertyChanged();
                }
            }
        }

        private Reservation _selectedReservation;
        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if (value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _buttCancel;
        public bool ButtCancel
        {
            get => _buttCancel;
            set
            {
                if (value != _buttCancel)
                {
                    _buttCancel = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _ownerRating;
        public string OwnerRating
        {
            get => _ownerRating;
            set
            {
                if (value != _ownerRating)
                {
                    _ownerRating = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _regularSelected;
        public bool RegularSelected
        {
            get { return _regularSelected; }
            set
            {
                if (value != _regularSelected)
                {
                    _regularSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _superSelected;
        public bool SuperSelected
        {
            get { return _superSelected; }
            set
            {
                if (value != _superSelected)
                {
                    _superSelected = value;
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

        private string _role;
        public string Role
        {
            get => _role;
            set
            {
                if (value != _role)
                {
                    _role = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accommodationNumber;
        public string AccommodationNumber
        {
            get => _accommodationNumber;
            set
            {
                if (value != _accommodationNumber)
                {
                    _accommodationNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _reservationNumber;
        public string ReservationNumber
        {
            get => _reservationNumber;
            set
            {
                if (value != _reservationNumber)
                {
                    _reservationNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private KeyValuePair<string, List<string>> _country;
        public KeyValuePair<string, List<string>> Country
        {
            get => _country;
            set
            {
                if (value.Key != _country.Key)
                {
                    _country = value;
                    OnPropertyChanged();
                    FillCityCb();
                }
            }
        }

        private string _accommodationType;
        public string AccommodationType
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancellationPeriod;
        public string CancellationPeriod
        {
            get => _cancellationPeriod;
            set
            {
                if (value != _cancellationPeriod)
                {
                    _cancellationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imageURLs;
        public string ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (value != _imageURLs)
                {
                    _imageURLs = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public OwnerMainViewModel(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository,
            ReservationService reservationService, GuestReviewService guestReviewService, UsersAccommodationService usersAccommodationService,
            OwnerReviewService ownerReviewService, PostponementService postponementService, User user,
            CancellationCsvCrudRepository cancellationCsvCrudRepository, UserService userService,
            NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            _user = user;

            _userService = userService;
            Username = _user.Username;
            Role = _user.Role.ToString();

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUserId(_user.getID()));

            AccommodationNumber = Accommodations.Count().ToString();

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            ReservedAccommodations = new ObservableCollection<Reservation>(_reservationService.GetUnreviewedReservations(_user.getID()));

            ReservationNumber = ReservedAccommodations.Count().ToString();

            _guestReviewService = guestReviewService;
            _guestReviewService.Subscribe(this);
            PastReservations = new ObservableCollection<GuestReview>(_guestReviewService.GetReviewedReservations(_user.getID()));

            Countries = new Dictionary<string, List<string>>(cityCountryCsvRepository.Load());
            Cities = new ObservableCollection<string>();

            _usersAccommodationService = usersAccommodationService;
            _ownerReviewService = ownerReviewService;
            _postponementService = postponementService;

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };

            #region Commands
            PublishCommand = new PublishAccommodationCommand(this, _accommodationService, _usersAccommodationService, _user);
            ResetCommand = new ResetCommand(this);
            AddImageCommand = new AddImageCommand(this);
            ClearURLsCommand = new ClearURLsCommand(this);

            NavigateToGuestReviewCommand = 
                new NavigateCommand(CreateGuestReviewNavigationService(modalNavigationStore), this, () => SelectedReservation != null && 
                    DateTime.Now >= SelectedReservation.EndDate && (DateTime.Now - SelectedReservation.EndDate.Date).TotalDays <= 5);
            NavigateToGuestReviewDetailsCommand =
                new NavigateCommand(CreateGuestReviewDetailsNavigationService(modalNavigationStore), this, () => SelectedReview != null);
            NavigateToOwnerReviewDetailsCommand =
                new NavigateCommand(CreateOwnerReviewDetailsNavigationService(modalNavigationStore));
            NavigateToPostponeRequestsCommand =
                new NavigateCommand(CreatePostponeRequestsNavigationService(modalNavigationStore));
            #endregion

            CalculateRating(_user.getID());

            NotificationTimer timer = new NotificationTimer(_user, null, ReservedAccommodations, _reservationService, _guestReviewService, cancellationCsvCrudRepository);
        }

        //ToDo: Resiti na bolji nacin: jokicev metod
        //ToDo: BUG
        private void FillCityCb()
        {
            Cities.Clear();

            if (Country.Key != null)
            {
                foreach (string city in Countries[Country.Key].ToList())
                    Cities.Add(city);
            }
        }

        //ToDo: Ne racunati neocenjene smestaje
        private void CalculateRating(int id)
        {
            double rating = _ownerReviewService.CalculateRating(id);
            OwnerRating = Math.Round(rating, 2).ToString();
            if (rating > 5.5 && PastReservations.Count() > 3)
            {
                RegularSelected = false;
                SuperSelected = true;
                _user.IsSuperUser = true;
                _userService.Update(_user);
            }
            else
            {
                SuperSelected = false;
                RegularSelected = true;
                _user.IsSuperUser = false;
                _userService.Update(_user);
            }
        }

        #region CreateNavigationServices
        private INavigationService CreateGuestReviewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<GuestReviewViewModel>
                (modalNavigationStore, () => new GuestReviewViewModel(_guestReviewService, _reservationService,
                    SelectedReservation, modalNavigationStore));
        }

        private INavigationService CreateGuestReviewDetailsNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<GuestReviewDetailsViewModel>
                (modalNavigationStore, () => new GuestReviewDetailsViewModel(SelectedReview, modalNavigationStore));
        }

        private INavigationService CreateOwnerReviewDetailsNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<OwnerReviewDetailsViewModel>
                (modalNavigationStore, () => new OwnerReviewDetailsViewModel(modalNavigationStore, _ownerReviewService, _user));
        }

        private INavigationService CreatePostponeRequestsNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<PostponeReservationViewModel>
                (modalNavigationStore, () => new PostponeReservationViewModel(modalNavigationStore, _postponementService, 
                    _reservationService, _user));
        }
        #endregion

        #region Update
        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        private void UpdateReservedAccommodations(List<Reservation> reservations)
        {
            ReservedAccommodations.Clear();
            foreach (var reservation in reservations)
                ReservedAccommodations.Add(reservation);
        }

        private void UpdatePastReservations(List<GuestReview> guestreviews)
        {
            PastReservations.Clear();
            foreach (var guestreview in guestreviews)
                PastReservations.Add(guestreview);
        }

        private void UpdateNumberOfRegisteredAccommodations()
        {
            AccommodationNumber = Accommodations.Count().ToString();
        }

        private void UpdateNumberOfReservedAccommodations()
        {
            ReservationNumber = ReservedAccommodations.Count().ToString();
        }

        private void UpdateOwnerRating(int id)
        {
            OwnerRating = _ownerReviewService.CalculateRating(id).ToString();
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetByUserId(_user.getID()));
            UpdateReservedAccommodations(_reservationService.GetUnreviewedReservations(_user.getID()));
            UpdatePastReservations(_guestReviewService.GetReviewedReservations(_user.getID()));
            UpdateNumberOfRegisteredAccommodations();
            UpdateNumberOfReservedAccommodations();
            UpdateOwnerRating(_user.getID());
        }
        #endregion

        #region Validation
        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "AccommodationName":
                        if (string.IsNullOrEmpty(AccommodationName))
                            result = "Accommodation name must not be empty";
                        break;
                    case "MaxGuests":
                        if (string.IsNullOrEmpty(MaxGuests))
                            result = "Max guests must not be empty";
                        else if (!int.TryParse(MaxGuests, out _))
                            result = "Max guests must be number";
                        break;
                    case "MinReservationDays":
                        if (string.IsNullOrEmpty(MinReservationDays))
                            result = "Min reservation days must not be empty";
                        else if (!int.TryParse(MinReservationDays, out _))
                            result = "Min reservation days must be number";
                        break;
                    case "CancellationPeriod":
                        if (string.IsNullOrEmpty(CancellationPeriod))
                            result = "Cancellation period must not be empty";
                        else if (!int.TryParse(CancellationPeriod, out _))
                            result = "Cancellation period must be number";
                        break;
                    case "ImageURLs":
                        if (string.IsNullOrEmpty(ImageURLs))
                            result = "You must add at least one image URL";
                        break;
                    case "City":
                        if (string.IsNullOrEmpty(City))
                            result = "City can not be empty";
                        break;
                    case "Country":
                        if (string.IsNullOrEmpty(Country.ToString()))
                            result = "Country can not be empty";
                        break;
                    case "AccommodationType":
                        if (string.IsNullOrEmpty(AccommodationType))
                            result = "Accommodation type can not be empty";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        #endregion
    }
}
