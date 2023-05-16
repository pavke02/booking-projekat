using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.UI.ViewModel.Guest2;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for Guest2GroupRide.xaml
    /// </summary>
    public partial class Guest2GroupRide : Window
    {
        public GroupRideViewModel GroupRideViewModel { get; set; }

        public Guest2GroupRide(User loggedUser, GroupRideService groupRideService)
        {
            InitializeComponent();

            GroupRideViewModel = new GroupRideViewModel(loggedUser,groupRideService);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if (GroupRideViewModel.Button_Click(int.Parse(NumberOFPassangersTextBox.Text),Street1TextBox.Text,City1TextBox.Text,Country1TextBox.Text, Street2TextBox.Text, City2TextBox.Text, Country2TextBox.Text,EndDateTextBox.Text, LangugageTextBox.Text))
            {
                Close();
            }
         
           
        }







    }
}
