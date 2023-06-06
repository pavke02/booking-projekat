using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;
using SIMS_Booking.Utility.Timers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class OwnerMainViewModel : ViewModelBase, IObserver, IDataErrorInfo, IPublish
    {
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;
        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly GuestReviewService _guestReviewService;
        private readonly UsersAccommodationService _usersAccommodationService;
        private readonly OwnerReviewService _ownerReviewService;
        private readonly PostponementService _postponementService;
        private readonly UserService _userService;
        private readonly RenovationAppointmentService _renovationAppointmentService;
        private readonly ForumService _forumService;
        private readonly CommentService _commentService;

        private readonly User _user;

        #region Commands
        public ICommand PublishCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand ClearURLsCommand { get; }
        public ICommand CancelRenovationCommand { get; }
        public ICommand NavigateToGuestReviewCommand { get; }
        public ICommand NavigateToGuestReviewDetailsCommand { get; }
        public ICommand NavigateToOwnerReviewDetailsCommand { get; }
        public ICommand NavigateToPostponeRequestsCommand { get; }
        public ICommand NavigateToAppointRenovatingCommand { get; }
        public ICommand NavigateToLocationsPopularityCommand { get; }
        public ICommand NavigateToForumView { get; }
        public ICommand GeneratePdfCommand { get; }

        #endregion

        #region Property                
        public List<string> TypesCollection { get; set; }
        public List<string> Countries { get; set; }
        public List<int> Years { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Forum> Forums { get; set; }
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        public ObservableCollection<GuestReview> PastReservations { get; set; }
        public ObservableCollection<RenovationAppointment> ActiveRenovations { get; set; }
        public ObservableCollection<RenovationAppointment> PastRenovations { get; set; }
        public Func<double, string> Formatter { get; set; }

        private string _xLabelName;
        public string XLabelName
        {
            get => _xLabelName;
            set
            {
                if (value != _xLabelName)
                {
                    _xLabelName = value;
                    OnPropertyChanged();
                }
            }
        }

        private Forum _selectedForum;
        public Forum SelectedForum
        {
            get => _selectedForum;
            set
            {
                if (value != _selectedForum)
                {
                    _selectedForum = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (value != _selectedYear)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                    GenerateStatsForMonths();
                }
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get => _labels;
            set
            {
                if (value != _labels)
                {
                    _labels = value;
                    OnPropertyChanged();
                }
            }
        }

        private SeriesCollection _statistics;
        public SeriesCollection Statistics
        {
            get => _statistics;
            set
            {
                if (value != _statistics)
                {
                    _statistics = value;
                    OnPropertyChanged();
                }
            }
        }

        private Accommodation _selectedAccommodationStats;
        public Accommodation SelectedAccommodationStats
        {
            get => _selectedAccommodationStats;
            set
            {
                if (value != _selectedAccommodationStats)
                {
                    _selectedAccommodationStats = value;
                    OnPropertyChanged();
                    GenerateStatsForYears();
                }
            }
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != null)
                {
                    _cities = value;
                    OnPropertyChanged();
                }
            }
        }

        private RenovationAppointment _selectedRenovation;
        public RenovationAppointment SelectedRenovation
        {
            get => _selectedRenovation;
            set
            {
                if (value != _selectedRenovation)
                {
                    _selectedRenovation = value;
                    OnPropertyChanged();
                }
            }
        }

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
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
            OwnerReviewService ownerReviewService, PostponementService postponementService, User user, UserService userService,
            RenovationAppointmentService renovationAppointmentService, ForumService forumService, CommentService commentService, ModalNavigationStore modalNavigationStore)
        {
            _user = user;
            _cityCountryCsvRepository = cityCountryCsvRepository;
            _usersAccommodationService = usersAccommodationService;
            _ownerReviewService = ownerReviewService;
            _postponementService = postponementService;
            _userService = userService;
            _commentService = commentService;
            _forumService = forumService;

            #region DataLoading
            Username = _user.Username;
            Role = _user.Role.ToString();

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUserId(_user.GetId()));
            AccommodationNumber = Accommodations.Count().ToString();

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            ReservedAccommodations = new ObservableCollection<Reservation>(_reservationService.GetUnratedActiveReservations(_user.GetId()));
            ReservationNumber = ReservedAccommodations.Count().ToString();

            _guestReviewService = guestReviewService;
            _guestReviewService.Subscribe(this);
            PastReservations = new ObservableCollection<GuestReview>(_guestReviewService.GetReviewedReservations(_user.GetId()));

            _forumService = forumService;
            Forums = new ObservableCollection<Forum>(_forumService.GetAll());

            _renovationAppointmentService = renovationAppointmentService;
            _renovationAppointmentService.Subscribe(this);
            ActiveRenovations =
                new ObservableCollection<RenovationAppointment>(_renovationAppointmentService.GetActiveRenovations(_user.GetId()));
            PastRenovations =
                new ObservableCollection<RenovationAppointment>(_renovationAppointmentService.GetPastRenovations(_user.GetId()));

            Countries = new List<string>(_cityCountryCsvRepository.LoadCountries());
            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };
            Years = new List<int>((Enumerable.Range(2000, DateTime.Now.Year - 1999).Reverse()).ToList());
            Cities = new ObservableCollection<string>(); 
            #endregion

            #region Commands
            PublishCommand = new PublishAccommodationCommand(this, _accommodationService, _usersAccommodationService, _user);
            ResetCommand = new ResetCommand(this);
            AddImageCommand = new AddImageCommand(this);
            ClearURLsCommand = new ClearURLsCommand(this);
            CancelRenovationCommand = new CancelRenovationCommand(this, _renovationAppointmentService);
            GeneratePdfCommand = new GeneratePdfCommand(_ownerReviewService, _user);

            NavigateToGuestReviewCommand = 
                new NavigateCommand(CreateGuestReviewNavigationService(modalNavigationStore), this, () => SelectedReservation != null && 
                    DateTime.Now >= SelectedReservation.EndDate && (DateTime.Now - SelectedReservation.EndDate.Date).TotalDays <= 5);
            NavigateToGuestReviewDetailsCommand =
                new NavigateCommand(CreateGuestReviewDetailsNavigationService(modalNavigationStore), this, () => SelectedReview != null);
            NavigateToOwnerReviewDetailsCommand =
                new NavigateCommand(CreateOwnerReviewDetailsNavigationService(modalNavigationStore));
            NavigateToPostponeRequestsCommand =
                new NavigateCommand(CreatePostponeRequestsNavigationService(modalNavigationStore));
            NavigateToAppointRenovatingCommand = 
                new NavigateCommand(CreateRenovationAppointingNavigationService(modalNavigationStore), this, () => SelectedAccommodation != null);
            NavigateToLocationsPopularityCommand = 
                new NavigateCommand(CreateLocationPopularityNavigationService(modalNavigationStore));
            NavigateToForumView = new NavigateCommand(CreateNavigateToForumViewNavigationService(modalNavigationStore));
            #endregion

            CalculateRating(_user.GetId());

            NotificationTimer timer = 
                new NotificationTimer(_user, null, ReservedAccommodations, _reservationService, _guestReviewService, _forumService);
            DatePassedTimer passedTimer =
                new DatePassedTimer(_accommodationService, _renovationAppointmentService, _user);

            Formatter = value => value.ToString("N");
        }

        private void FillCityCb()
        {
            Cities.Clear();           
            if(Country != null)
                Cities = _cityCountryCsvRepository.LoadCitiesForCountry(Country);            
        }

        private void CalculateRating(int id)
        {
            double rating = _ownerReviewService.CalculateRating(id);
            OwnerRating = Math.Round(rating, 2).ToString();
            if (rating > 9.5 && PastReservations.Count() > 50)
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

        #region Statistics
        private void GenerateStatsForYears()
        {
            if (SelectedAccommodationStats != null)
            {
                Dictionary<string, int> reservationCount = new Dictionary<string, int>(_reservationService.GetReservationsByYear(SelectedAccommodationStats.GetId()));
                Dictionary<string, int> cancellationsCount = new Dictionary<string, int>(_reservationService.GetCancellationsByYear(SelectedAccommodationStats.GetId()));
                Dictionary<string, int> postponementCount = new Dictionary<string, int>(_postponementService.GetPostponementsByYear(SelectedAccommodationStats.GetId()));
                Dictionary<string, int> renovationsCount = new Dictionary<string, int>(_ownerReviewService.GetRenovationsByYear(SelectedAccommodationStats.GetId()));

                List<string> sortedLabels = reservationCount.Keys
                    .Union(postponementCount.Keys)
                    .Union(renovationsCount.Keys).
                    Union(cancellationsCount.Keys).ToList();
                sortedLabels.Sort();
                Labels = sortedLabels.ToArray();

                GenerateStats(reservationCount, postponementCount, renovationsCount, cancellationsCount);
                XLabelName = "Years";
            }
        }

        private void GenerateStatsForMonths()
        {
            if (SelectedAccommodationStats != null)
            {
                Dictionary<int, int> reservationCount = new Dictionary<int, int>(_reservationService.GetReservationsByMonth(SelectedAccommodationStats.GetId(), int.Parse(SelectedYear)));
                Dictionary<int, int> cancellationsCount = new Dictionary<int, int>(_reservationService.GetCancellationsByMonth(SelectedAccommodationStats.GetId(), int.Parse(SelectedYear)));
                Dictionary<int, int> postponementCount = new Dictionary<int, int>(_postponementService.GetPostponementsByMonth(SelectedAccommodationStats.GetId(), int.Parse(SelectedYear)));
                Dictionary<int, int> renovationsCount = new Dictionary<int, int>(_ownerReviewService.GetRenovationsByMonth(SelectedAccommodationStats.GetId(), int.Parse(SelectedYear)));

                List<int> sortedLabels = reservationCount.Keys
                    .Union(postponementCount.Keys)
                    .Union(renovationsCount.Keys).
                    Union(cancellationsCount.Keys).ToList();
                sortedLabels.Sort();
                //pretvara mesec(iz broja) u ime meseca(npr. 1 = januar)
                Labels = sortedLabels.Select(e => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(e)).ToArray();

                //mapira dictionary<int, int> na dictionary<string, int>
                GenerateStats(reservationCount.ToDictionary(kvp => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key), kvp => kvp.Value),
                    postponementCount.ToDictionary(kvp => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key), kvp => kvp.Value),
                    renovationsCount.ToDictionary(kvp => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key), kvp => kvp.Value),
                    cancellationsCount.ToDictionary(kvp => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(kvp.Key), kvp => kvp.Value));
                XLabelName = "Months";
            }
        }

        private void GenerateStats(Dictionary<string, int> reservationCount, Dictionary<string, int> postponementCount, Dictionary<string, int> renovationsCount, Dictionary<string, int> cancellationsCount)
        {
            ChartValues<int> reservationsChartValues = new ChartValues<int>();
            ChartValues<int> cancellationsChartValues = new ChartValues<int>();
            ChartValues<int> postponementsChartValues = new ChartValues<int>();
            ChartValues<int> renovationsChartValues = new ChartValues<int>();
            foreach (string label in Labels)
            {
                reservationsChartValues.Add(reservationCount.ContainsKey(label) ? reservationCount[label] : 0);
                postponementsChartValues.Add(postponementCount.ContainsKey(label) ? postponementCount[label] : 0);
                renovationsChartValues.Add(renovationsCount.ContainsKey(label) ? renovationsCount[label] : 0);
                cancellationsChartValues.Add(cancellationsCount.ContainsKey(label) ? cancellationsCount[label] : 0);
            }

            Statistics = new SeriesCollection { new ColumnSeries
            {
                Title = "Reservations",
                Values = reservationsChartValues
            }};

            Statistics.Add(new ColumnSeries
            {
                Title = "Postponements",
                Values = postponementsChartValues
            });

            Statistics.Add(new ColumnSeries
            {
                Title = "Renovations",
                Values = renovationsChartValues
            });

            Statistics.Add(new ColumnSeries
            {
                Title = "Cancellation",
                Values = cancellationsChartValues
            });
        }
        #endregion

        #region CreateNavigationServices
        private INavigationService CreateNavigateToForumViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<ForumViewModel>
                (modalNavigationStore, () => new ForumViewModel(_commentService, _accommodationService, _forumService.GetById(SelectedForum.GetId()), _user, modalNavigationStore));
        }
        private INavigationService CreateLocationPopularityNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<LocationPopularityViewModel>
                (modalNavigationStore, () => new LocationPopularityViewModel(_reservationService, _accommodationService, _usersAccommodationService, _user, modalNavigationStore));
        }
        private INavigationService CreateRenovationAppointingNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<RenovationAppointingViewModel>
                (modalNavigationStore, () => new RenovationAppointingViewModel(SelectedAccommodation, _reservationService, _renovationAppointmentService, _accommodationService, modalNavigationStore));
        }

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

        private void UpdateActiveRenovations(List<RenovationAppointment> activeRenovations)
        {
            ActiveRenovations.Clear();
            foreach (var activeRenovation in activeRenovations)
                ActiveRenovations.Add(activeRenovation);
        }

        private void UpdatePastRenovations(List<RenovationAppointment> pastRenovations)
        {
            PastRenovations.Clear();
            foreach (var pastRenovation in pastRenovations)
                PastRenovations.Add(pastRenovation);
        }

        private void UpdateNumberOfReservedAccommodations()
        {
            ReservationNumber = ReservedAccommodations.Count().ToString();
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetByUserId(_user.GetId()));
            UpdateReservedAccommodations(_reservationService.GetUnratedActiveReservations(_user.GetId()));
            UpdatePastReservations(_guestReviewService.GetReviewedReservations(_user.GetId()));
            UpdateActiveRenovations(_renovationAppointmentService.GetActiveRenovations(_user.GetId()));
            UpdatePastRenovations(_renovationAppointmentService.GetPastRenovations(_user.GetId()));
            UpdateNumberOfRegisteredAccommodations();
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
                        if (string.IsNullOrEmpty(Country))
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
