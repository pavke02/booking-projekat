using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.UI.ViewModel.Guest2;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for Guest2GuideReviewView.xaml
    /// </summary>
    public partial class Guest2GuideReviewView : Window
    {
       


        
        GuideReviewViewModel GuideReviewViewModel { get; set; }

        public Guest2GuideReviewView(GuideReviewService guideReviewService, ReservedTourService reservedTourService, TourReservation tourReservation)
        {
            InitializeComponent();
            GuideReviewViewModel = new GuideReviewViewModel(guideReviewService,reservedTourService,tourReservation);


            DataContext = GuideReviewViewModel;

            



        }
        
        private void TextBoxCheck(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
                submitButton.IsEnabled = true;
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            GuideReviewViewModel.SubmitReview(imageTb.Text);
            Close();
        }

        private void ImageTbCheck(object sender, TextChangedEventArgs e)
        {
            addURLButton.Visibility = Visibility.Hidden;

            if (GuideReviewViewModel.ImageTbCheck(urlTb.Text))
            {
                addURLButton.Visibility = Visibility.Visible;
            }

        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            
            if (imageTb.Text == "")
                imageTb.Text = urlTb.Text;
            else
                imageTb.Text += "\n" + urlTb.Text;
            urlTb.Clear();
        }

        private void ClearURLs(object sender, RoutedEventArgs e)
        {
            imageTb.Clear();
            GuideReviewViewModel.ClearURLs();
        }

        private void imageTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
    }
}
