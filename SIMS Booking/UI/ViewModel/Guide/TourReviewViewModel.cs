using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class TourReviewViewModel:ViewModelBase,IObserver
    {
        public TourReview _tourReview;
        public ConfirmTourService _confirmTourService;
        public Tour _tour { get; set; }
        public TourReviewService _tourReviewService { get; set; }
        public ObservableCollection<TourReview> Reviews { get; set; }
        public TourReview SelectedReview { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourReviewViewModel(TourReview tourReview, TourReviewService tourReviewService, Tour tour,
            ConfirmTourService confirmTourService, TourReview selected)
        {
            _tourReview = tourReview;
            _tourReviewService = tourReviewService;
            _tour = tour;
            _confirmTourService = confirmTourService;
            SelectedReview = selected;
            _tourReviewService.Subscribe(this);

            Reviews = new ObservableCollection<TourReview>(_tourReviewService.recenzije(confirmTourService, tour));
        }

        private void UpdateTourReview(List<TourReview> tours)
        {
            Reviews.Clear();
            foreach (var tour in tours)
                Reviews.Add(tour);
        }

        public void Update()
        {
            UpdateTourReview(_tourReviewService.GetAll());
        }

        [RelayCommand]
        private void IsValidReview()
        {
            if (SelectedReview != null)
            {
                _tourReviewService.updateIsValidReview(SelectedReview);
            }
        }
    }
}
