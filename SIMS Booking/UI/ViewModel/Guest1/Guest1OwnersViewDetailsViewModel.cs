using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    public class Guest1OwnersViewDetailsViewModel : ViewModelBase
    {
        private readonly GuestReviewService _guestReviewService;
        private readonly User _loggedUser;

        private int _tidiness;

        public int Tidiness
        {
            get => _tidiness;
            set
            {
                if (value != _tidiness)
                {
                    _tidiness = value;
                    OnPropertyChanged();
                }
            }
        }
        private int _ruleFollowing;

        public int RuleFollowing
        {
            get => _ruleFollowing;
            set
            {
                if (value != _ruleFollowing)
                {
                    _ruleFollowing = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _comment;

        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _accommodationTitle;

        public string AccommodationTitle
        {
            get => _accommodationTitle;
            set
            {
                if (value != _accommodationTitle)
                {
                    _accommodationTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateBackCommand { get; }

        public Guest1OwnersViewDetailsViewModel(ModalNavigationStore modalNavigationStore, GuestReviewService guestReviewService, Reservation selectedReservation, User loggedUser)
        {
            _guestReviewService = guestReviewService;
            _loggedUser = loggedUser;
            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));

            foreach (GuestReview guestReview in _guestReviewService.GetReviewedReservations(selectedReservation.Accommodation.User.GetId()))
            {
                if (selectedReservation.GetId() == guestReview.ReservationId)
                {

                    Tidiness = guestReview.Tidiness;
                    RuleFollowing = guestReview.RuleFollowing;
                    Comment = guestReview.Comment;
                    AccommodationTitle = selectedReservation.Accommodation.Name;
                }
            }

        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
