using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Service;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for TourReviewView.xaml
    /// </summary>
    public partial class TourReviewView : Window, IObserver, INotifyPropertyChanged
    {
        public TourReview _tourReview;
        public ConfirmTourService _confirmTourService;
        public Tour _tour {  get; set; }
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



        public TourReviewView(TourReview tourReview, TourReviewService tourReviewService, Tour tour,ConfirmTourService confirmTourService,TourReview selected)
        {
            InitializeComponent();
            DataContext = this;
            _tourReview = tourReview;
            _tourReviewService = tourReviewService;
            _tour = tour;
            _confirmTourService = confirmTourService;
            SelectedReview = selected;
            _tourReviewService.Subscribe(this);

            Reviews = new ObservableCollection<TourReview>(_tourReviewService.recenzije(confirmTourService, tour));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void IsValidReview(object sender, RoutedEventArgs e)
        {
            if(SelectedReview != null)
            {
                _tourReviewService.updateIsValidReview(SelectedReview);
            }
        }
    }
}
