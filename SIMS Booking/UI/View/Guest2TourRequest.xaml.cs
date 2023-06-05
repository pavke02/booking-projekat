using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.UI.ViewModel.Guest2;
using System;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for Guest2TourRequest.xaml
    /// </summary>
    public partial class Guest2TourRequest : Window
    {
        
        public NewTourRequestViewModel NewTourRequest { get; set; }

        public Guest2TourRequest(User loggedUser, TourRequestService tourRequestService)
        {
            InitializeComponent();

            NewTourRequest = new NewTourRequestViewModel(loggedUser,tourRequestService);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NewTourRequest.TourRequest_Click(CityTextBox.Text,CountryTextBox.Text,DescriptionTextBox.Text,LangugageTextBox.Text,GuestsTextBox.Text,StartDateTextBox.Text, EndDateTextBox.Text))
            {
                Close();
            }
       
        }



    }
}
