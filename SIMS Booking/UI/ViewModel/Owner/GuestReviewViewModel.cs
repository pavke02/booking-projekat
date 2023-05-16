using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class GuestReviewViewModel : ViewModelBase
    {
        private GuestReviewService _guestReviewService;
        private ReservationService _reservationService;
        private Reservation _reservation;

        public ICommand SubmitReviewCommand { get; }
        public ICommand NavigateBackCommand { get; }

        #region Property
        private bool _errorText;
        public bool ErrorText
        {
            get => _errorText;
            set
            {
                if (value != _errorText)
                {
                    _errorText = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tidiness = 0;
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    if (value > 5)
                        tidiness = 5;
                    else if (value < 1)
                        tidiness = 1;
                    else
                        tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ruleFollowing = 0;
        public int RuleFollowing
        {
            get => ruleFollowing;
            set
            {
                if (value != ruleFollowing)
                {
                    if (value > 5)
                        ruleFollowing = 5;
                    else if (value < 1)
                        ruleFollowing = 1;
                    else
                        ruleFollowing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public GuestReviewViewModel(GuestReviewService guestReviewService, ReservationService reservationService, Reservation reservation, ModalNavigationStore modalNavigationStore)
        {
            _reservation = reservation;

            _guestReviewService = guestReviewService;
            _reservationService = reservationService;

            SubmitReviewCommand =
                new SubmitReviewCommand(CreateCloseModalNavigationService(modalNavigationStore),this, _guestReviewService, _reservationService, _reservation);
            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        #region Validation
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required";
                }
                return null;
            }
        }
        #endregion
    }
}
