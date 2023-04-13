using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility;

namespace SIMS_Booking.View
{

    public partial class Guest1MainView : Window, IObserver
    {

        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public List<Accommodation> AccommodationsFiltered { get; set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }        
        public ObservableCollection<Reservation> UserReservations { get; set; }
        public ObservableCollection<Postponement> UserPostponements { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public Reservation SelectedReservation { get; set; }
        public User LoggedUser { get; set; }

        private readonly AccommodationService _accommodationService;
        private readonly CityCountryCsvRepository _cityCountryCsvRepository;
        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        private readonly PostponementService _postponementService;
        private readonly CancellationCsvCrudRepository _cancellationCsvCrudRepository;
        private readonly OwnerReviewService _ownerReviewService;

        #region Properties
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        public Guest1MainView(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository, ReservationService reservationService, ReservedAccommodationService reservedAccommodationService, User loggedUser, PostponementService postponementService, CancellationCsvCrudRepository cancellationCsvCrudRepository, OwnerReviewService ownerReviewService)
        {

            LoggedUser = loggedUser;

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            //Accommodations = new ObservableCollection<Accommodation>(_accommodationService.SortBySuperOwner(_accommodationService.GetAll()));
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            UserReservations = new ObservableCollection<Reservation>(_reservationService.GetReservationsByUser(loggedUser.getID()));

            _postponementService = postponementService;
            NotificationTimer timer = new NotificationTimer(LoggedUser, _postponementService);
            _postponementService.Subscribe(this);
            UserPostponements = new ObservableCollection<Postponement>(_postponementService.GetPostponementsByUser(loggedUser.getID()));

            _ownerReviewService = ownerReviewService;

            _cityCountryCsvRepository = cityCountryCsvRepository;
            _cancellationCsvCrudRepository = cancellationCsvCrudRepository;

            _reservedAccommodationService = reservedAccommodationService;

            AccommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            Countries = new Dictionary<string, List<string>>(_cityCountryCsvRepository.Load());

            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            AccommodationName = "";

            TypesCollection = new List<string>
            {
                "Apartment",
                "House",
                "Cottage"
            };

            if (!loggedUser.IsSuperUser)
                UserTb.Text = LoggedUser.Username + ", Guest";
            else
                UserTb.Text = LoggedUser.Username + ", Super Guest";

        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            Guest1ReservationView reservationView =
                new Guest1ReservationView(SelectedAccommodation, LoggedUser, _reservationService, _reservedAccommodationService);
            reservationView.Show();
        }

        private void OpenGallery(object sender, RoutedEventArgs e)
        {
            Guest1GalleryView galleryView = new Guest1GalleryView(SelectedAccommodation);
            galleryView.Show();
        }

        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        private void UpdateUserReservations(List<Reservation> reservations)
        {
            UserReservations.Clear();
            foreach (var reservation in reservations)
                UserReservations.Add(reservation);
        }

        private void UpdateUserPostponements(List<Postponement> postponements)
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
            UpdateUserReservations(_reservationService.GetReservationsByUser(LoggedUser.getID()).ToList());
            UpdateUserPostponements(_postponementService.GetPostponementsByUser(LoggedUser.getID()).ToList());
        }

        public void CancelReservation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation.StartDate - DateTime.Today <
                TimeSpan.FromDays(SelectedReservation.Accommodation.CancellationPeriod))
            {
                MessageBox.Show("It is not possible to cancel reservation after cancellation period.");
                return;
            }
            List<Reservation> newReservations = _reservationService.GetAll();
            foreach (Reservation reservation in newReservations)
            {
                if (reservation.getID() == SelectedReservation.getID())
                {
                    _postponementService.DeletePostponementsByReservationId(reservation.getID());
                    _reservationService.DeleteCancelledReservation(reservation.getID());
                    _cancellationCsvCrudRepository.Save(reservation);
                    newReservations.Remove(reservation);
                    UpdateUserReservations(newReservations);
                    UpdateUserPostponements(_postponementService.GetAll().ToList());
                    return;
                }
            }
        }

        private void ChangeReservation(object sender, RoutedEventArgs e)
        {
            Guest1ChangeReservationView guest1ChangeReservationView =
                new Guest1ChangeReservationView(SelectedReservation, LoggedUser, _reservationService,
                    _reservedAccommodationService, _postponementService);
            guest1ChangeReservationView.Show();
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetAllTabs();

            if (TabC.SelectedIndex == 0)
            {
                FiltersGrid.Visibility = Visibility.Visible;
                AccommodationsButtonsGrid.Visibility = Visibility.Visible;
                AccommodationsTab.Height += 10;
            }
            else if (TabC.SelectedIndex == 1)
            {
                ReservationsPanel.Visibility = Visibility.Visible;
                ReservationsTab.Height += 10;
            }
            else
            {
                PostponementsTab.Height += 10;
            }
        }

        private void ResetAllTabs()
        {
            AccommodationsTab.Height = 50;
            ReservationsTab.Height = 50;
            PostponementsTab.Height = 50;
            ReservationsPanel.Visibility = Visibility.Collapsed;
            FiltersGrid.Visibility = Visibility.Collapsed;
            AccommodationsButtonsGrid.Visibility = Visibility.Collapsed;
        }




        private void ReviewReservation(object sender, RoutedEventArgs e)
        {
            Guest1OwnerReviewView reviewView = new Guest1OwnerReviewView(_ownerReviewService, _reservationService, SelectedReservation);
            reviewView.Show();
        }

        private void ReservationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                ReviewButton.IsEnabled = false;
                CancelReservationButton.IsEnabled = false;
                ChangeReservationButton.IsEnabled = false;
                return;
            }

            CancelReservationButton.IsEnabled = true;
            ChangeReservationButton.IsEnabled = true;

            if (DateTime.Today - SelectedReservation.EndDate > TimeSpan.FromDays(5) || DateTime.Today - SelectedReservation.EndDate < TimeSpan.FromDays(0))
            {
                ReviewButton.IsEnabled = false;
            }
            else
            {
                ReviewButton.IsEnabled = true;
            }
        }

        private void ChangeCities(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            cityCb.Items.Clear();
            if (countryCb.SelectedIndex != -1)
            {
                foreach (string city in Countries.ElementAt(countryCb.SelectedIndex).Value)
                {
                    cityCb.Items.Add(city).ToString();
                }
            }

            List<Accommodation> accommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            UpdateKindsState();

            foreach (Accommodation accommodation in Accommodations)
            {
                bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName == "") && (Country.Key == accommodation.Location.Country || countryCb.SelectedIndex == -1)
                    && (accommodation.Location.City == City || cityCb.SelectedIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= Convert.ToInt32(MaxGuests) || MaxGuests == null)
                    && (accommodation.MinReservationDays <= Convert.ToInt32(MinReservationDays) || MinReservationDays == null);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            //UpdateAccommodationsDataGrid(_accommodationService.SortBySuperOwner(accommodationsFiltered));
            UpdateAccommodationsDataGrid(accommodationsFiltered);

        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ClearFilterFields();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    // ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = _accommodationService.SortBySuperOwner(_accommodationService.GetAll());
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = _accommodationService.GetAll();
                }
            }
        }

        private void ClearFilterFields()
        {
            nameTb.Clear();
            AccommodationName = "";
            countryCb.SelectedItem = null;
            Country = new KeyValuePair<string, List<string>>();
            cityCb.SelectedItem = null;
            City = "";
            HouseCheckBox.IsChecked = true;
            ApartmentCheckBox.IsChecked = true;
            CottageCheckBox.IsChecked = true;
            MaxGuests = "0";
            maxGuestsTb.Clear();
            MinReservationDays = "10";
            minReservationDaysTb.Clear();
        }

        private void ApplyFilters(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            List<Accommodation> accommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            if (AccommodationName == null)
                AccommodationName = "";

            UpdateKindsState();
            if (MaxGuests == "")
            {
                MaxGuests = "0";
            }

            if (MinReservationDays == "")
            {
                MinReservationDays = "10";
            }

            foreach (Accommodation accommodation in Accommodations)
            {
                bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName == "") && (Country.Key == accommodation.Location.Country || countryCb.SelectedIndex == -1)
                    && (accommodation.Location.City == City || cityCb.SelectedIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= Convert.ToInt32(MaxGuests) || MaxGuests == null)
                    && (accommodation.MinReservationDays <= Convert.ToInt32(MinReservationDays) || MinReservationDays == null);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            //UpdateAccommodationsDataGrid(_accommodationService.SortBySuperOwner(accommodationsFiltered));
            UpdateAccommodationsDataGrid(accommodationsFiltered);
        }

        private void ApplyCityChange(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            List<Accommodation> accommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            UpdateKindsState();

            if (AccommodationName == null)
                AccommodationName = "";

            if (MaxGuests == "")
            {
                MaxGuests = "0";
            }

            if (MinReservationDays == "")
            {
                MinReservationDays = "10";
            }

            foreach (Accommodation accommodation in Accommodations)
            {
                bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName == "") && (Country.Key == accommodation.Location.Country || countryCb.SelectedIndex == -1)
                    && (accommodation.Location.City == City || cityCb.SelectedIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= Convert.ToInt32(MaxGuests) || MaxGuests == null)
                    && (accommodation.MinReservationDays <= Convert.ToInt32(MinReservationDays) || MinReservationDays == null);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            //UpdateAccommodationsDataGrid(_accommodationService.SortBySuperOwner(accommodationsFiltered));
            UpdateAccommodationsDataGrid(accommodationsFiltered);
        }

        private void UpdateKindsState()
        {
            Kinds = new List<AccommodationType>();
            Kinds.Clear();  
            if (HouseCheckBox.IsChecked == true || HouseCheckBox == null)
                Kinds.Add(AccommodationType.House);
            if (ApartmentCheckBox != null)
            {
                if (ApartmentCheckBox.IsChecked == true)
                    Kinds.Add(AccommodationType.Apartment);
            }

            if (CottageCheckBox != null)
            {
                if (CottageCheckBox.IsChecked == true || CottageCheckBox == null)
                    Kinds.Add(AccommodationType.Cottage);
            }
            

        }

        private void UpdateAccommodationsDataGrid(List<Accommodation> accommodationsFiltered)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = accommodationsFiltered;

                }
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            List<Accommodation> accommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            UpdateKindsState();
            if (AccommodationName == null)
                AccommodationName = "";

            if (MaxGuests == "")
            {
                MaxGuests = "0";
            }

            if (MinReservationDays == "")
            {
                MinReservationDays = "10";
            }

            foreach (Accommodation accommodation in Accommodations)
            {
                bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName == "") && (Country.Key == accommodation.Location.Country || countryCb.SelectedIndex == -1)
                    && (accommodation.Location.City == City || cityCb.SelectedIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= Convert.ToInt32(MaxGuests) || MaxGuests == null)
                    && (accommodation.MinReservationDays <= Convert.ToInt32(MinReservationDays) || MinReservationDays == null);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            //UpdateAccommodationsDataGrid(_accommodationService.SortBySuperOwner(accommodationsFiltered));
            UpdateAccommodationsDataGrid(accommodationsFiltered);
        }

        private void CloseAllWindowsButSignIn()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() != typeof(SignInForm))
                {
                    window.Close();
                }
            }

        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
            CloseAllWindowsButSignIn();
        }
    }
}
