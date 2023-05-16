using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class GuestReviewDetailsViewModel : ViewModelBase
    {
        #region Property
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

        private string _startDate;
        public string StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _endDate;
        public string EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

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
        #endregion

        public ICommand NavigateBackCommand { get; }

        public GuestReviewDetailsViewModel(GuestReview review, ModalNavigationStore modalNavigationStore)
        {
            Username = review.Reservation.User.Username;
            AccommodationName = review.Reservation.Accommodation.Name;
            StartDate = review.Reservation.StartDate.ToShortDateString();
            EndDate = review.Reservation.EndDate.ToShortDateString();
            Tidiness = review.Tidiness;
            RuleFollowing = review.RuleFollowing;
            Comment = review.Comment;

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
