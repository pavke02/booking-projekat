using Microsoft.TeamFoundation.Common;
using SIMS_Booking.Commands.Guest1Commands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Enums;
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
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SIMS_Booking.UI.View.Guest1;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    public class Guest1MainViewModel : ViewModelBase, IObserver, IDataErrorInfo
    {

        #region Collections

        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public List<Accommodation> AccommodationsFiltered { get; set; }

        public ObservableCollection<Postponement> UserPostponements { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;
        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly PostponementService _postponementService;
        private readonly OwnerReviewService _ownerReviewService;
        private readonly RenovationAppointmentService _renovationAppointmentService;
        private readonly GuestReviewService _guestReviewService;
        #endregion

        #region Properties

        private ObservableCollection<Reservation> _userReservations;

        public ObservableCollection<Reservation> UserReservations
        {
            get => _userReservations;
            set
            {
                if (value != _userReservations)
                {
                    _userReservations = value;
                    OnPropertyChanged();
                    SetSuperGuest();
                }
            }
        }

        private ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userTb;
        public string UserTb
        {
            get => _userTb;
            set
            {
                if (value != _userTb)
                {
                    _userTb = value;
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
                    ApplyFilters();
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
                    ApplyFilters();
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
                    ApplyFilters();
                    FillCityCb();
                    OnPropertyChanged();
                }
            }
        }

        private List<AccommodationType> _kinds;

        public List<AccommodationType> Kinds
        {
            get => _kinds;
            set
            {
                if (value != _kinds)
                {
                    _kinds = value;
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
                    ApplyFilters();
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
                    ApplyFilters();
                    OnPropertyChanged();

                }
            }
        }

        private bool _isRenovated;
        public bool IsRenovated
        {
            get => _isRenovated;
            set
            {
                if (value != _isRenovated)
                {
                    _isRenovated  = value;
                    OnPropertyChanged();
        
                }
            }
        }

        private int _selectedTab;

        public int SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (value != _selectedTab)
                {
                    _selectedTab = value;
                    ResetAllTabs();

                    if (_selectedTab == 0)
                    {
                        FiltersPanelVisibility = Visibility.Visible;
                        AccommodationsPanelVisibility = Visibility.Visible;
                    }
                    else if (_selectedTab == 1)
                    {
                        ReservationsPanelVisibility = Visibility.Visible;
                    }
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _reservationsPanelVisibility;
        public Visibility ReservationsPanelVisibility
        {
            get => _reservationsPanelVisibility;
            set
            {
                if (value != _reservationsPanelVisibility)
                {
                    _reservationsPanelVisibility = value;
                    OnPropertyChanged();

                }
            }
        }

        private Visibility _filtersPanelVisibility;
        public Visibility FiltersPanelVisibility
        {
            get => _filtersPanelVisibility;
            set
            {
                if (value != _filtersPanelVisibility)
                {
                    _filtersPanelVisibility = value;
                    OnPropertyChanged();

                }
            }
        }

        private Visibility _accommodationsPanelVisibility;
        public Visibility AccommodationsPanelVisibility
        {
            get => _accommodationsPanelVisibility;
            set
            {
                if (value != _accommodationsPanelVisibility)
                {
                    _accommodationsPanelVisibility = value;
                    OnPropertyChanged();

                }
            }
        }

        private bool _reviewEnabled;
        public bool ReviewEnabled
        {
            get => _reviewEnabled;
            set
            {
                if (value != _reviewEnabled)
                {
                    _reviewEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _cancelEnabled;
        public bool CancelEnabled
        {
            get => _cancelEnabled;
            set
            {
                if (value != _cancelEnabled)
                {
                    _cancelEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _changeEnabled;
        public bool ChangeEnabled
        {
            get => _changeEnabled;
            set
            {
                if (value != _changeEnabled)
                {
                    _changeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _ownersReviewEnabled;
        public bool OwnersReviewEnabled
        {
            get => _ownersReviewEnabled;
            set
            {
                if (value != _ownersReviewEnabled)
                {
                    _ownersReviewEnabled = value;
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
                    ReservationChanged();
                    OnPropertyChanged();
                }
            }
        }

        private bool _houseChecked;

        public bool HouseChecked
        {
            get => _houseChecked;
            set
            {
                if (value != _houseChecked)
                {
                    _houseChecked = value;
                    ApplyFilters();
                    OnPropertyChanged();
                }
            }
        }
        private bool _apartmentChecked;

        public bool ApartmentChecked
        {
            get => _apartmentChecked;
            set
            {
                if (value != _apartmentChecked)
                {
                    _apartmentChecked = value;
                    ApplyFilters();
                    OnPropertyChanged();
                }
            }
        }
        private bool _cottageChecked;

        public bool CottageChecked
        {
            get => _cottageChecked;
            set
            {
                if (value != _cottageChecked)
                {
                    _cottageChecked = value;
                    ApplyFilters();
                    OnPropertyChanged();
                }
            }
        }

        private int _cityIndex;

        public int CityIndex
        {
            get => _cityIndex;
            set
            {
                if (value != _cityIndex)
                {
                    _cityIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _countryIndex;

        public int CountryIndex
        {
            get => _countryIndex;
            set
            {
                if (value != _countryIndex)
                {
                    _countryIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _citiesSource;

        public List<string> CitiesSource
        {
            get => _citiesSource;
            set
            {
                if (value != _citiesSource)
                {
                    _citiesSource = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _generatePDFEnabled;

        public bool GeneratePDFEnabled
        {
            get => _generatePDFEnabled;
            set
            {
                if (value != _generatePDFEnabled)
                {
                    _generatePDFEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guest1WheneverWhereverViewModel Guest1WheneverWhereverViewModel;

        #endregion

        #region Commands

        public ICommand NavigateToReserveCommand { get; }
        public ICommand NavigateToGalleryCommand { get; }
        public ICommand NavigateToChangeReservationCommand { get; }
        public ICommand NavigateToReservationReviewCommand { get; }
        public ICommand NavigateToOwnersReviewCommand { get; }
        public ICommand NavigateToWheneverCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CancelReservationCommand { get; }
        public ICommand GeneratePDFCommand { get; }
        public ICommand MainDemoCommand { get; }

        #endregion

        public Guest1MainViewModel(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository, ReservationService reservationService, 
            ReservedAccommodationService reservedAccommodationService, User loggedUser, PostponementService postponementService, 
            OwnerReviewService ownerReviewService, RenovationAppointmentService renovationAppointmentService, GuestReviewService guestReviewService, ModalNavigationStore modalNavigationStore)

        {

            LoggedUser = loggedUser;

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.SortBySuperOwner(_accommodationService.GetAll()));

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            UserReservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsByUser(loggedUser.GetId()));

            _postponementService = postponementService;
            NotificationTimer timer = new NotificationTimer(LoggedUser, _postponementService);
            _postponementService.Subscribe(this);
            UserPostponements = new ObservableCollection<Postponement>(_postponementService.GetPostponementsByUser(loggedUser.GetId()));

            _ownerReviewService = ownerReviewService;
            _guestReviewService = guestReviewService;
            _cityCountryCsvRepository = cityCountryCsvRepository;

            _reservedAccommodationService = reservedAccommodationService;
            _renovationAppointmentService = renovationAppointmentService;

            AccommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            Countries = new Dictionary<string, List<string>>(_cityCountryCsvRepository.Load());

            AccommodationName = "";
            CountryIndex = -1;
            CityIndex = -1;
            CottageChecked = true;
            ApartmentChecked = true;
            HouseChecked = true;

            TypesCollection = new List<string>
            {
                "Apartment",
                "House",
                "Cottage"
            };

            CitiesSource = new List<string>();



            SetSuperGuest();

            NavigateToReserveCommand =
                new NavigateCommand(CreateReservationViewNavigationService(modalNavigationStore));
            NavigateToGalleryCommand = 
                new NavigateCommand(CreateGalleryViewNavigationService(modalNavigationStore));
            NavigateToChangeReservationCommand =
                new NavigateCommand(CreateChangeReservationViewNavigationService(modalNavigationStore));
            NavigateToReservationReviewCommand =
                new NavigateCommand(CreateOwnerReviewNavigationService(modalNavigationStore));
            NavigateToOwnersReviewCommand = 
                new NavigateCommand(CreateOwnersReviewNavigationService(modalNavigationStore));
            NavigateToWheneverCommand = 
                new NavigateCommand(CreateWheneverNavigationService(modalNavigationStore));
            ResetCommand = new ResetFiltersCommand(this);
            CancelReservationCommand = new CancelReservationCommand(_reservationService, this);
            GeneratePDFCommand = new GeneratePDFCommand(this);
            MainDemoCommand = new MainDemoCommand(this);

             DatePassedTimer datePassedTimer =
                 new DatePassedTimer(_accommodationService, _renovationAppointmentService, loggedUser);
             _renovationAppointmentService = renovationAppointmentService;

             FiltersPanelVisibility = Visibility.Visible;
             AccommodationsPanelVisibility = Visibility.Visible;
             ReservationsPanelVisibility = Visibility.Collapsed;

             Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        private INavigationService CreateReservationViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1ReservationViewModel>(modalNavigationStore,
                () => new Guest1ReservationViewModel(modalNavigationStore, SelectedAccommodation, LoggedUser, _reservationService,
                    _reservedAccommodationService, this, Guest1WheneverWhereverViewModel));
        }

        private INavigationService CreateGalleryViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1GalleryViewModel>(modalNavigationStore,
                () => new Guest1GalleryViewModel(modalNavigationStore,SelectedAccommodation));
        }

        private INavigationService CreateChangeReservationViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1ChangeReservationViewModel>(modalNavigationStore,
                () => new Guest1ChangeReservationViewModel(modalNavigationStore, SelectedReservation, LoggedUser, _reservationService, _reservedAccommodationService, _postponementService));
        }

        private INavigationService CreateOwnerReviewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1OwnerReviewViewModel>(modalNavigationStore,
                () => new Guest1OwnerReviewViewModel(modalNavigationStore, _ownerReviewService, _reservationService, SelectedReservation));
        }

        private INavigationService CreateOwnersReviewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1OwnersViewDetailsViewModel>(modalNavigationStore,
                () => new Guest1OwnersViewDetailsViewModel(modalNavigationStore ,_guestReviewService,  SelectedReservation, LoggedUser));
        }

        private INavigationService CreateWheneverNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1WheneverWhereverViewModel>(modalNavigationStore,
                () => new Guest1WheneverWhereverViewModel(modalNavigationStore, _accommodationService, _reservationService, _reservedAccommodationService, LoggedUser));
        }

        public void SetSuperGuest()
        {
            UserTb = LoggedUser.Username + ", Guest";
            if (_reservationService.IsSuperGuest(LoggedUser))
            {
                UserTb = LoggedUser.Username + ", Super Guest";
            }
        }


        public void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        public void UpdateUserReservations(List<Reservation> reservations)
        {
            UserReservations.Clear();
            foreach (var reservation in reservations)
                UserReservations.Add(reservation);
        }

        public void UpdateUserPostponements(List<Postponement> postponements)
        {
            UserPostponements.Clear();
            foreach (var postponement in postponements)
            {
                UserPostponements.Add(postponement);
            }
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetAll());
            UpdateUserReservations(_reservationService.GetReservationsByUser(LoggedUser.GetId()).ToList());
            UpdateUserPostponements(_postponementService.GetPostponementsByUser(LoggedUser.GetId()).ToList());
        }

        public void CancelReservation(object sender, RoutedEventArgs e)
        {
            
        }

        private void ResetAllTabs()
        {
            AccommodationsPanelVisibility = Visibility.Collapsed;
            FiltersPanelVisibility = Visibility.Collapsed;
            ReservationsPanelVisibility = Visibility.Collapsed;
        }


        private void ReservationChanged()
        {
            if (SelectedReservation == null)
            {
                ReviewEnabled = false;
                CancelEnabled= false;
                ChangeEnabled = false;
                GeneratePDFEnabled = false;
                return;
            }

            CancelEnabled = true;
            ChangeEnabled = true;
            GeneratePDFEnabled = true;

            if (DateTime.Today - SelectedReservation.EndDate > TimeSpan.FromDays(5) || DateTime.Today - SelectedReservation.EndDate < TimeSpan.FromDays(0) || SelectedReservation.HasGuestReviewed)
                ReviewEnabled = false;
            else
                ReviewEnabled = true;

            if (SelectedReservation.HasOwnerReviewed && SelectedReservation.HasGuestReviewed)
                OwnersReviewEnabled = true;
            else
               OwnersReviewEnabled = false;
        }

        private void FillCityCb()
        {
            CitiesSource.Clear();
            if (CountryIndex != -1)
                CitiesSource = _cityCountryCsvRepository.LoadCitiesForCountry(Country.Key).ToList();
        }

        private void ApplyFilters()
        {
            ObservableCollection<Accommodation> accommodationsFiltered = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;
            int maxGuests = 0;
            int minReservationDays = 0;
            if (AccommodationName == null)
                AccommodationName = "";

            if (MinReservationDays == "")
                minReservationDays = 10;
            else
                minReservationDays = Convert.ToInt32(MinReservationDays);

            if (MaxGuests == "")
                maxGuests = 1;
            else
                maxGuests = Convert.ToInt32(MaxGuests);

            UpdateKindsState();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());

                foreach (Accommodation accommodation in Accommodations)
            {bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName.IsNullOrEmpty()) && (Country.Key == accommodation.Location.Country || CountryIndex == -1)
                    && (accommodation.Location.City == City || CityIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= maxGuests || MaxGuests.IsNullOrEmpty())
                    && (accommodation.MinReservationDays <= minReservationDays || MinReservationDays.IsNullOrEmpty());

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            MinReservationDays = "";
            MaxGuests = "";

            Accommodations = accommodationsFiltered;
        }

        private void UpdateKindsState()
        {
            Kinds = new List<AccommodationType>();
            Kinds.Clear();
            if (HouseChecked)
                    Kinds.Add(AccommodationType.House);
            if (ApartmentChecked)
                    Kinds.Add(AccommodationType.Apartment);
            if (CottageChecked)
                    Kinds.Add(AccommodationType.Cottage);
        }

        public string Error { get; }

        public string this[string columnName] => throw new NotImplementedException();
    }
}
