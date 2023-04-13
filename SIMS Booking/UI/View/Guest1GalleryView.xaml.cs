using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIMS_Booking.Model;

namespace SIMS_Booking.UI.View
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
