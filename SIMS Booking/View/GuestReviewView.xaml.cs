using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMS_Booking.View
{
 
    public partial class GuestReviewView : Window, IDataErrorInfo
    {
        private GuestReviewRepository _guestReviewRepository;
        private ReservedAccommodationRepository _reservedAccommodationRepository;
        private ReservationRepository _reservationRepository;
        private Reservation _reservation;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuestReviewView(GuestReviewRepository guestReviewRepository, ReservedAccommodationRepository reservedAccommodationRepository, ReservationRepository reservationRepository, Reservation reservation)
        {
            InitializeComponent();
            DataContext = this;            

            _reservation = reservation;

            _guestReviewRepository = guestReviewRepository;
            _reservedAccommodationRepository = reservedAccommodationRepository;
            _reservationRepository = reservationRepository;
        }        

        private void TextBoxCheck(object sender, RoutedEventArgs e)
        {
            submitButton.IsEnabled = false;
            if(IsValid)
            {
                submitButton.IsEnabled = true;
            }
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            _guestReviewRepository.SubmitReview(Tidiness, RuleFollowing, Comment, _reservation);
            _reservationRepository.Update(_reservation);            
            Close();
        }

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
    }    
}
