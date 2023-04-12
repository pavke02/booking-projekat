using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service;

namespace SIMS_Booking.View.Owner
{
 
    public partial class GuestReviewView : Window, IDataErrorInfo
    {
        private GuestReviewService _guestReviewService;        
        private ReservationService _reservationService;
        private Reservation _reservation;

        #region Property
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
                    if(value > 5)                   
                        ruleFollowing = 5;                    
                    else if(value < 1)                    
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestReviewView(GuestReviewService guestReviewService, ReservationService reservationService, Reservation reservation)
        {
            InitializeComponent();
            DataContext = this;            

            _reservation = reservation;

            _guestReviewService = guestReviewService;            
            _reservationService = reservationService;
        }

        private void TextBoxCheck(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            submitButton.IsEnabled = false;
            if (IsValid)            
                submitButton.IsEnabled = true;            
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            _guestReviewService.SubmitReview(Tidiness, RuleFollowing, Comment, _reservation);
            _reservationService.Update(_reservation);            
            Close();
        }

        #region Validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required";
                }
                return null;
            }
        }

        private readonly string[] validatedProperties = { "Comment" };

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
