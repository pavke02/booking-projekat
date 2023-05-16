using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SIMS_Booking.UI.View
{

    public partial class Guest2ReservedTours : Window, IObserver
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<TourReservation> TourReservations { get; set; }
        public TourReservation SelectedTourReservation { get; set; }
        public User LoggedUser { get; set; }


        private readonly TourService _tourService;
        private readonly TourReservation _tourReservation;
        private readonly GuideReviewService _guideReviewService;
        private readonly ReservedTourService _reservedTourService;

        public Guest2ReservedTours(User loggedUser, GuideReviewService guideReviewService, ReservedTourService reservedTourService)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _reservedTourService = reservedTourService;

            TourReservations = new ObservableCollection<TourReservation>(_reservedTourService.GetAll());
            _guideReviewService = guideReviewService;

        }

        private void Review_Tour(object sender, RoutedEventArgs e)
        {
            Guest2GuideReviewView reviewView =
                new Guest2GuideReviewView(_guideReviewService, _reservedTourService, SelectedTourReservation);

            reviewView.Show();

        }

        private void Back(object sender, RoutedEventArgs e)
        {
         
            Close();

        }

        private void UpdateTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (var tour in tours)
                Tours.Add(tour);
        }

        public void Update()
        {
            UpdateTours(_tourService.GetAll());
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}
