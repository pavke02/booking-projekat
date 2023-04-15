using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class OwnerMainView : UserControl, INotifyPropertyChanged
    {        
        #region Property
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

        public OwnerMainView()
        {
            InitializeComponent();
        }    
        
        //ToDo: ne racunati neocenjene smestaje
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

        #region Buttons
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
        #endregion

        #region ButtonValidations
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
        #endregion
    }
}
