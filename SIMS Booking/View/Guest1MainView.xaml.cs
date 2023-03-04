using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.View
{
    public partial class Guest1MainView : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public ObservableCollection<Accommodation> AccommodationsReorganized { get; set; }

        private AccomodationRepository _accommodationRepository;
        private CityCountryRepository _cityCountryRepository;



        public Guest1MainView(AccomodationRepository accommodationRepository, CityCountryRepository cityCountryRepository)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            _accommodationRepository = accommodationRepository;
            _cityCountryRepository = cityCountryRepository; 

            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.Load());
        }

        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView(_accommodationRepository, _cityCountryRepository);
            filterView.Show();
        }

        public void UpdateFilters(ObservableCollection<Accommodation> accommodations)
        {
            DataGridAccommodations.ItemsSource = accommodations;
        }

        private void Reserve(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenGallery(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
