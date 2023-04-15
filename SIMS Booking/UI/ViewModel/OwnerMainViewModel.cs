using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMS_Booking.Utility;
using System.ComponentModel;
using SIMS_Booking.Utility.Observer;
using System.Linq;
using System.Windows.Input;
using SIMS_Booking.Utility.Commands.OwnerCommands;
using System;

namespace SIMS_Booking.UI.ViewModel
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

        #region Property                
        public Dictionary<string, List<string>> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public List<string> TypesCollection { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        public ObservableCollection<GuestReview> PastReservations { get; set; }
        public Reservation SelectedReservation { get; set; }
        public GuestReview SelectedReview { get; set; }        

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

        private string _cancelationPeriod;
        public string CancelationPeriod
        {
            get => _cancelationPeriod;
            set
            {
                if (value != _cancelationPeriod)
                {
                    _cancelationPeriod = value;
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
            NavigationStore navigationStore)
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
            #endregion

            CalculateRating(_user.getID());

            NotificationTimer timer = new NotificationTimer(_user, null, ReservedAccommodations, _reservationService, _guestReviewService, cancellationCsvCrudRepository);
        }

        //ToDo: Resiti na bolji nacin: jokicev metod
        private void FillCityCb()
        {
            Cities.Clear();

            if (Country.Key != null)
            {
                foreach (string city in Countries[Country.Key].ToList())
                    Cities.Add(city);
            }
        }

        //ToDo: Ne racunati neocenjene
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

        private void UpdateNumberOfRegisterdAccommodations()
        {
            AccommodationNumber = Accommodations.Count().ToString();
        }

        private void UpdateNumberOfReservedAccommodations()
        {
            ReservationNumber = ReservedAccommodations.Count().ToString();
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetByUserId(_user.getID()));
            UpdateReservedAccommodations(_reservationService.GetUnreviewedReservations(_user.getID()));
            UpdatePastReservations(_guestReviewService.GetReviewedReservations(_user.getID()));
            UpdateNumberOfRegisterdAccommodations();
            UpdateNumberOfReservedAccommodations();
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
                    case "CancelationPeriod":
                        if (string.IsNullOrEmpty(CancelationPeriod))
                            result = "Cancelation period must not be empty";
                        else if (!int.TryParse(CancelationPeriod, out _))
                            result = "Cancelation period must be number";
                        break;
                    case "ImageURLs":
                        if (string.IsNullOrEmpty(ImageURLs))
                            result = "You must add atleast one image URL";
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
