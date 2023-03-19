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
