using SIMS_Booking.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMS_Booking.View
{
    public partial class ReviewDetailsView : Window, INotifyPropertyChanged
    {        
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReviewDetailsView(GuestReview review)
        {
            InitializeComponent();
            DataContext = this;

            Username = review.Reservation.User.Username;
            AccommodationName = review.Reservation.Accommodation.Name;
            StartDate = review.Reservation.StartDate.ToShortDateString();
            EndDate = review.Reservation.EndDate.ToShortDateString();
        }
    }
}
