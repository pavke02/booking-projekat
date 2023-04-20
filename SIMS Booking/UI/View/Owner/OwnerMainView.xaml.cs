using System.Collections.ObjectModel;
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
        private void ViewPostponeRequests(object sender, RoutedEventArgs e)
        {
            PostponeReservationView postponeReservationView = new PostponeReservationView(_postponementService, _reservationService, _user);
            postponeReservationView.ShowDialog();
        }
        #endregion
    }
}
