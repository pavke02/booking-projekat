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

    public partial class Guest1GalleryView : Window
    {
        public Accommodation SelectedAccommodation { get; set; }   
        public List<string> imageUrlsLeft { get; set; }
        public List<string> imageUrlsRight { get; set; }

        public Guest1GalleryView(Accommodation selectedAccommodation)
        {
            InitializeComponent();

            SelectedAccommodation = selectedAccommodation;

            LoadImages();

            imageList.ItemsSource = imageUrlsLeft;
            imageList2.ItemsSource = imageUrlsRight;

        }

        private void LoadImages()
        {
            imageUrlsLeft = new List<string>();
            imageUrlsRight = new List<string>();

            for (int i = 0; i < SelectedAccommodation.ImageURLs.Count / 2; i++)
            {
                imageUrlsLeft.Add(SelectedAccommodation.ImageURLs.ElementAt(i));
            }
            for (int i = SelectedAccommodation.ImageURLs.Count / 2; i < SelectedAccommodation.ImageURLs.Count; i++)
            {
                imageUrlsRight.Add(SelectedAccommodation.ImageURLs.ElementAt(i));
            }
        }
    }
}
