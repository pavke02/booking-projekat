using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.UI.ViewModel.Guest2;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View
{
    public partial class Guest2FindingTaxiFast : Window
    {
        public FindingTaxiFastViewModel FindingTaxiFastViewModel { get; set; }

        public Guest2FindingTaxiFast(User _loggedUser, VehicleReservationService vehicleReservationService, DriverLocationsService driverLocationsService)
        {
            InitializeComponent();

            FindingTaxiFastViewModel = new FindingTaxiFastViewModel(_loggedUser, vehicleReservationService, driverLocationsService);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (FindingTaxiFastViewModel.Button_Click(TextBoxCity.Text, StartingAddressTextBox.Text, FinalAddressTextBox.Text, TimeofDepartureTextBox.Text))
            {
                Close();
            }
        }
    }
}
