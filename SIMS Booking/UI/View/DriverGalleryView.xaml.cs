using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SIMS_Booking.Model;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for DriverGalleryView.xaml
    /// </summary>
    public partial class DriverGalleryView : Window
    {
        public Vehicle Vehicle { get; set; }

        public DriverGalleryView(Vehicle vehicle)
        {
            InitializeComponent();
            Vehicle = vehicle;
            List<string> imageUrls = new List<string>();
            List<string> imageUrls2 = new List<string>();

            for (int i = 0; i < vehicle.ImagesURL.Count / 2; i++)
            {
                imageUrls.Add(vehicle.ImagesURL.ElementAt(i));
            }
            for (int i = vehicle.ImagesURL.Count / 2; i < vehicle.ImagesURL.Count; i++)
            {
                imageUrls2.Add(vehicle.ImagesURL.ElementAt(i));
            }

            imageList.ItemsSource = imageUrls;
            imageList2.ItemsSource = imageUrls2;
        }
    }
}
