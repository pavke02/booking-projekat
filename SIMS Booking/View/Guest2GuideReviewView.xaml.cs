using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2GuideReviewView.xaml
    /// </summary>
    public partial class Guest2GuideReviewView : Window
    {

        Guest2GuideReviewViewModel guest2GuideReviewViewModel { get; set; }

        public Guest2GuideReviewView(GuideReviewService guideReviewService, ReservedTourService reservedTourService, TourReservation tourReservation)
        {
            InitializeComponent();
            guest2GuideReviewViewModel = new Guest2GuideReviewViewModel(guideReviewService,reservedTourService,tourReservation);


            DataContext = guest2GuideReviewViewModel;

            



        }
        
        private void TextBoxCheck(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
                submitButton.IsEnabled = true;
        }

        private void SubmitReview(object sender, RoutedEventArgs e)
        {
            guest2GuideReviewViewModel.SubmitReview(imageTb.Text);
            Close();
        }

        private void ImageTbCheck(object sender, TextChangedEventArgs e)
        {
            addURLButton.Visibility = Visibility.Hidden;

            if (guest2GuideReviewViewModel.ImageTbCheck(urlTb.Text))
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
            guest2GuideReviewViewModel.ClearURLs();
        }
        
    }
}
