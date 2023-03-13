using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest1GalleryView.xaml
    /// </summary>
    public partial class Guest1GalleryView : Window
    {
        public Accommodation SelectedAccommodation { get; set; }          

        public Guest1GalleryView(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            SelectedAccommodation = selectedAccommodation;
            List<string> imageUrls = new List<string>();
            List<string> imageUrls2 = new List<string>();

            for (int i = 0; i < selectedAccommodation.ImageURLs.Count / 2; i++)
            {
                imageUrls.Add(selectedAccommodation.ImageURLs.ElementAt(i));
            }
            for (int i = selectedAccommodation.ImageURLs.Count/2; i < selectedAccommodation.ImageURLs.Count; i++)
            {
                imageUrls2.Add(selectedAccommodation.ImageURLs.ElementAt(i));
            }

            imageList.ItemsSource = imageUrls;
            imageList2.ItemsSource = imageUrls2;


        }
    }
}
