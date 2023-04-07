using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Service;

namespace SIMS_Booking.View.Owner
{    
    public partial class OwmerReviewDetailsVeiw : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<OwnerReview> OwnersReviews { get; set; }
        public OwnerReview SelectedReview { get; set; }

        private OwnerReviewService _ownerReviewService;
        private User _user;

        #region Property
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

        private int _ownersCorrectness;
        public int OwnersCorrectness
        {
            get => _ownersCorrectness;
            set
            {
                if (value != _ownersCorrectness)
                {
                    _ownersCorrectness = value;
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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OwmerReviewDetailsVeiw(OwnerReviewService ownerReviewService, User user)
        {
            InitializeComponent();
            DataContext = this;

            _user = user;

            _ownerReviewService = ownerReviewService;
            _ownerReviewService.Subscribe(this);
            OwnersReviews = new ObservableCollection<OwnerReview>(_ownerReviewService.GetShowableReviews(_user.getID()));
        }

        private void ShowReviewDetails(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            details.Visibility = Visibility.Visible;
            Tidiness = SelectedReview.Tidiness;
            OwnersCorrectness = SelectedReview.OwnersCorrectness;
            Comment = SelectedReview.Comment;
        }

        #region Update
        private void UpdateOwnersReviews(List<OwnerReview> ownerReviews)
        {
            OwnersReviews.Clear();
            foreach (var ownerReview in ownerReviews)
                OwnersReviews.Add(ownerReview);
        }

        public void Update()
        {
            UpdateOwnersReviews(_ownerReviewService.GetByUserId(_user.getID()));
        }
        #endregion
    }
}
