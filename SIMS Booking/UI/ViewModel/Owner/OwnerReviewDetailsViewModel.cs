using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class OwnerReviewDetailsViewModel : ViewModelBase, IObserver
    {
        public ICommand NavigateBackCommand { get; }

        #region Property
        public ObservableCollection<OwnerReview> OwnersReviews { get; set; }

        private OwnerReviewService _ownerReviewService;
        private User _user;

        private OwnerReview _selectedReview;
        public OwnerReview SelectedReview
        {
            get => _selectedReview;
            set
            {
                if (value != _selectedReview)
                {
                    _selectedReview = value;
                    OnPropertyChanged();
                    ShowReview();
                }
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
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

        private int _renovationLevel;
        public int RenovationLevel
        {
            get => _renovationLevel;
            set
            {
                if (value != _renovationLevel)
                {
                    _renovationLevel = value;
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

        public OwnerReviewDetailsViewModel(ModalNavigationStore modalNavigationStore, OwnerReviewService ownerReviewService, User user)
        {
            _user = user;

            _ownerReviewService = ownerReviewService;
            _ownerReviewService.Subscribe(this);
            OwnersReviews = new ObservableCollection<OwnerReview>(_ownerReviewService.GetShowableReviews(_user.GetId()));
            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private void ShowReview()
        {
            IsVisible = true;
            Tidiness = SelectedReview.Tidiness;
            OwnersCorrectness = SelectedReview.OwnersCorrectness;
            Comment = SelectedReview.Comment;
            RenovationLevel = SelectedReview.RenovationLevel;
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
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
            UpdateOwnersReviews(_ownerReviewService.GetByUserId(_user.GetId()));
        }
        #endregion
    }
}
