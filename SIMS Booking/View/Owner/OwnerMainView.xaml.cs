using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility;

namespace SIMS_Booking.View.Owner
{
    public partial class OwnerMainView : Window, IObserver, INotifyPropertyChanged, IDataErrorInfo
    {        
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        public ObservableCollection<GuestReview> PastReservations { get; set; }

        public Reservation SelectedReservation { get; set; }
        public GuestReview SelectedReview { get; set; }
                
        private readonly User _user;

        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly GuestReviewService _guestReviewService;        
        private readonly UsersAccommodationService _usersAccommodationService;
        private readonly OwnerReviewService _ownerReviewService;
        private readonly PostponementService _postponementService;
        private readonly UserService _userService;

        #region Property
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }                

        public OwnerMainView(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository, ReservationService reservationService, GuestReviewService guestReviewService, UsersAccommodationService usersAccommodationService, OwnerReviewService ownerReviewService, PostponementService postponementService, User user, CancellationCsvCrudRepository cancellationCsvCrudRepository, UserService userService)
        {
            InitializeComponent();
            DataContext = this;            

            _userService = userService;
            _user = user;
            usernameTb.Text = _user.Username;
            roleTb.Text = _user.Role.ToString();

            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetByUserId(_user.getID()));

            accommodationNumberTb.Text = Accommodations.Count().ToString();

            _reservationService = reservationService;
            _reservationService.Subscribe(this);
            ReservedAccommodations = new ObservableCollection<Reservation>(_reservationService.GetUnreviewedReservations(_user.getID()));

            reservationNumberTb.Text = ReservedAccommodations.Count().ToString();

            _guestReviewService = guestReviewService;
            _guestReviewService.Subscribe(this);
            PastReservations = new ObservableCollection<GuestReview>(_guestReviewService.GetReviewedReservations(_user.getID()));

            Countries = new Dictionary<string, List<string>>(cityCountryCsvRepository.Load());
            
            _usersAccommodationService = usersAccommodationService;
            _ownerReviewService = ownerReviewService;
            _postponementService = postponementService;

            CalculateRating(_user.getID());

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };

            NotificationTimer timer = new NotificationTimer(_user, null, ReservedAccommodations, _reservationService, _guestReviewService, cancellationCsvCrudRepository);   
        }           
        
        private void CalculateRating(int id)
        {
            double rating = _ownerReviewService.CalculateRating(id);
            ownerRatingTb.Text = Math.Round(rating, 2).ToString();
            if(rating > 5.5 && PastReservations.Count() > 3)
            {
                regularCb.IsChecked = false;
                superCb.IsChecked = true;
                _user.IsSuperUser = true;
                _userService.Update(_user);
            }                
            else
            {
                superCb.IsChecked = false;
                regularCb.IsChecked = true;
                _user.IsSuperUser = false;
                _userService.Update(_user);
            }                
        }

        private void FillCityCb(object sender, SelectionChangedEventArgs e)
        {
            cityCb.Items.Clear();

            if (countryCb.SelectedIndex != -1)
            {
                foreach (string city in Countries.ElementAt(countryCb.SelectedIndex).Value)
                    cityCb.Items.Add(city).ToString();
            }
        }

        #region Buttons
        private void Publish(object sender, RoutedEventArgs e)
        {
            Location location = new Location(Country.Key, City);

            List<string> imageURLs = new List<string>();
            string[] values = ImageURLs.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);
            
            Accommodation accommodation = new Accommodation(AccommodationName, location, (AccommodationType)Enum.Parse(typeof(AccommodationType), AccommodationType), _user, int.Parse(MaxGuests), int.Parse(MinReservationDays), int.Parse(CancelationPeriod), imageURLs);
            _accommodationService.Save(accommodation);

            UsersAccommodation usersAccommodation = new UsersAccommodation(_user.getID(), accommodation.getID());
            _usersAccommodationService.Save(usersAccommodation);
            MessageBox.Show("Accommodation published successfully");
            ClearTextBoxes();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {            
            if (imageTb.Text == "")            
                ImageURLs = urlTb.Text;            
            else            
                ImageURLs = imageTb.Text + "\n" + urlTb.Text;            

            urlTb.Clear();
        }

        private void RateGuest(object sender, RoutedEventArgs e)
        {
            GuestReviewView guestReviewView = new GuestReviewView(_guestReviewService, _reservationService, _reservationService.GetById(SelectedReservation.getID()));
            guestReviewView.ShowDialog();
            CalculateRating(_user.getID());
        }

        private void ShowReview(object sender, RoutedEventArgs e)
        {
            GuestReviewDetailsView detailsView = new GuestReviewDetailsView(SelectedReview);
            detailsView.ShowDialog();
        }

        private void ShowOwnersReviews(object sender, RoutedEventArgs e)
        {
            OwmerReviewDetailsVeiw owmerReviewDetails = new OwmerReviewDetailsVeiw(_ownerReviewService, _user);
            owmerReviewDetails.ShowDialog();
        }

        private void ViewPostponeRequests(object sender, RoutedEventArgs e)
        {
            PostponeReservationView postponeReservationView = new PostponeReservationView(_postponementService, _reservationService, _user);
            postponeReservationView.ShowDialog();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearURLs(object sender, RoutedEventArgs e)
        {
            imageTb.Clear();
            ImageURLs = "";    
            publishButton.IsEnabled = false;
        }
        #endregion

        #region ButtonValidations
        private void IsPublishable(object sender, RoutedEventArgs e)
        {
            publishButton.IsEnabled = IsValid;            
        }

        private void IsReviewable(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation != null)            
                if (DateTime.Now >= SelectedReservation.EndDate && (DateTime.Now - SelectedReservation.EndDate.Date).TotalDays <= 5)
                    reviewGuestButton.IsEnabled = true;
                else
                    reviewGuestButton.IsEnabled = false;
        }

        private void IsShowable(object sender, SelectionChangedEventArgs e)
        {
            reviewDetails.IsEnabled = SelectedReview != null;            
        }
        
        private void ImageTbCheck(object sender, TextChangedEventArgs e)
        {
            addURLButton.Visibility = Visibility.Hidden;
            if (!string.IsNullOrEmpty(urlTb.Text) && !string.IsNullOrWhiteSpace(urlTb.Text) && Uri.IsWellFormedUriString(urlTb.Text, UriKind.Absolute))
            {
                addURLButton.Visibility = Visibility.Visible;
            }
        }
        #endregion

        private void ClearTextBoxes()
        {
            accommodationNameTb.Clear();
            maxGuestsTb.Clear();
            minReservationDaysTb.Clear();
            cancelationPeriodTb.Clear();
            imageTb.Clear();
            ImageURLs = "";
            countryCb.SelectedItem = null;
            cityCb.SelectedItem = null;
            typeCb.SelectedItem = null;
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

        private void UpdatePastReservations(List<GuestReview> guestReviews)
        {
            PastReservations.Clear();
            foreach (var guestReview in guestReviews)
                PastReservations.Add(guestReview);
        }

        private void UpdateNumberOfRegisterdAccommodations()
        {
            accommodationNumberTb.Text = Accommodations.Count().ToString();
        }

        private void UpdateNumberOfReservedAccommodations()
        {
            reservationNumberTb.Text = ReservedAccommodations.Count().ToString();
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
                switch(columnName)
                {
                    case "AccommodationName":
                        if (string.IsNullOrEmpty(AccommodationName))
                            result = "Accommodation name must not be empty";
                        break;
                    case "MaxGuests":
                        if(string.IsNullOrEmpty(MaxGuests))
                            result = "Max guests must not be empty";
                        else if(!int.TryParse(MaxGuests, out _))
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
                        if(string.IsNullOrEmpty(City))
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

        private readonly string[] validatedProperties = { "AccommodationName", "MaxGuests", "MinReservationDays", "CancelationPeriod", "ImageURLs", "City", "Country", "AccommodationType" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        #endregion
    }
}
