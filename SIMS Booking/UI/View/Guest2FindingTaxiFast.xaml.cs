using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.UI.ViewModel;

namespace SIMS_Booking.UI.View
{
    public partial class Guest2FindingTaxiFast : Window
    {
        public Guest2FindingTaxiFastViewModel guest2FindingTaxiFastViewModel { get; set; }

        public Guest2FindingTaxiFast(User _loggedUser)
        {
            InitializeComponent();
            
            guest2FindingTaxiFastViewModel = new Guest2FindingTaxiFastViewModel(_loggedUser);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (guest2FindingTaxiFastViewModel.Button_Click(TextBoxCity.Text, StartingAddressTextBox.Text, FinalAddressTextBox.Text, TimeofDepartureTextBox.Text))
            {
                Close();    
            }
        }
    }
}
