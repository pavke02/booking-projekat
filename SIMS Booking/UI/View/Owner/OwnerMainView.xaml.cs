using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class OwnerMainView : UserControl
    {        
        #region Property
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
        #endregion

        public OwnerMainView()
        {
            InitializeComponent();
        }

        #region Buttons
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
