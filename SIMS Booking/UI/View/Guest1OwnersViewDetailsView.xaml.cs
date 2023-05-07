using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_Booking.Model;
using SIMS_Booking.Service;

namespace SIMS_Booking.View
{

    public partial class Guest1OwnersViewDetailsView : Window
    {
        private readonly GuestReviewService _guestReviewService;
        private readonly User _loggedUser;

        public int Tidiness;
        public int RuleFollowing;
        public string Comment;

        public Guest1OwnersViewDetailsView(GuestReviewService guestReviewService, Reservation selectedReservation, User loggedUser)
        {
            InitializeComponent();
            _guestReviewService = guestReviewService;
            _loggedUser = loggedUser;
            foreach (GuestReview guestReview in _guestReviewService.GetReviewedReservations(selectedReservation.Accommodation.User.getID()))
            {
                if (selectedReservation.getID() == guestReview.ReservationId)
                {

                    tidinessTb.Text = guestReview.Tidiness.ToString();
                    tidinessSl.Value = guestReview.Tidiness;
                    ruleFollowingTb.Text = guestReview.RuleFollowing.ToString();
                    ruleFollowingSl.Value = guestReview.RuleFollowing;
                    commentTb.Text = guestReview.Comment;
                    ReviewTitle.Content = selectedReservation.Accommodation.Name;

                }
            }

        }
    }
}
