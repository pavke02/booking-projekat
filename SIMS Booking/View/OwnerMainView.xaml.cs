﻿using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Observer;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.State;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.Xml.Linq;
using Microsoft.TeamFoundation.Common;

namespace SIMS_Booking.View
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
                
        private User _user;

        private AccommodationService _accommodationService;
        private CityCountryRepository _cityCountryRepository;
        private ReservationService _reservationService;
        private GuestReviewService _guestReviewService;
        private ReservedAccommodationService _reservedAccommodationService;
        private UsersAccommodationService _usersAccommodationService;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }                

        public OwnerMainView(AccommodationService accommodationService, CityCountryRepository cityCountryRepository, ReservationService reservationService, GuestReviewService guestReviewService, ReservedAccommodationService reservedAccommodationService, UsersAccommodationService usersAccommodationService, User user)
        {
            InitializeComponent();
            DataContext = this;            

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

            _cityCountryRepository = cityCountryRepository;
            Countries = new Dictionary<string, List<string>>(_cityCountryRepository.Load());

            _reservedAccommodationService = reservedAccommodationService;
            _usersAccommodationService = usersAccommodationService;

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };

            Timer timer = new Timer(ReservedAccommodations, _reservationService, _guestReviewService);   
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

        private void ShowReview(object sender, RoutedEventArgs e)
        {
            ReviewDetailsView detailsView = new ReviewDetailsView(SelectedReview);
            detailsView.ShowDialog();
        }

        private void RateGuest(object sender, RoutedEventArgs e)
        {
            GuestReviewView guestReviewView = new GuestReviewView(_guestReviewService, _reservationService, _reservationService.GetById(SelectedReservation.getID()));
            guestReviewView.ShowDialog();
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

        private void IsPublishable(object sender, RoutedEventArgs e)
        {
            publishButton.IsEnabled = false;
            if (IsValid)
            {
                publishButton.IsEnabled = true;
            }
        }

        private void IsReviewable(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation != null)            
                if (DateTime.Now >= SelectedReservation.EndDate && (DateTime.Now - SelectedReservation.EndDate.Date).TotalDays <= 5)
                    reviewGuestClick.IsEnabled = true;
                else
                    reviewGuestClick.IsEnabled = false;
        }

        private void IsShowable(object sender, SelectionChangedEventArgs e)
        {
            reviewDetails.IsEnabled = false;
            if (SelectedReview != null)
                reviewDetails.IsEnabled = true;
        }

        //ToDo: srediti da radi samo za validne URLove
        private void ImageTbCheck(object sender, TextChangedEventArgs e)
        {
            addURLButton.Visibility = Visibility.Hidden;
            if (!string.IsNullOrEmpty(urlTb.Text) && !string.IsNullOrWhiteSpace(urlTb.Text) && Uri.IsWellFormedUriString(urlTb.Text, UriKind.Absolute))
            {
                addURLButton.Visibility = Visibility.Visible;
            }
        }

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

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetByUserId(_user.getID()));
            UpdateReservedAccommodations(_reservationService.GetUnreviewedReservations(_user.getID()));
            UpdatePastReservations(_guestReviewService.GetReviewedReservations(_user.getID()));
        }

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
    }

    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           //ToDo: Srediti da radi za promenu dana// Po mogucnosti staviti u poseban namespace
           return DateTime.Now >= (DateTime)value && (DateTime.Now - (DateTime)value).TotalDays <= 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException();
        }
    }    
}
