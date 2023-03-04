using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using SIMS_Booking.Repository;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest1MainView.xaml
    /// </summary>
    public partial class Guest1MainView : Window
    {
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        public ObservableCollection<Accommodation> AccommodationsReorganized { get; set; }

        private AccomodationRepository Repository;



        public Guest1MainView(AccomodationRepository accommodationRepository)
        {
            InitializeComponent();
            DataContext = this;
            DataGridAccommodations.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;

            Accommodations = new ObservableCollection<Accommodation>(accommodationRepository.Load());

        }


        private void AddFilters(object sender, RoutedEventArgs e)
        {
            Guest1FilterView filterView = new Guest1FilterView();
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
