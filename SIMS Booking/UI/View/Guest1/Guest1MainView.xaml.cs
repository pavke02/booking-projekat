using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMS_Booking.UI.View.Guest1
{

    public partial class Guest1MainView : UserControl
    {
        public Guest1MainView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {

            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private void MainDemoClick(object sender, RoutedEventArgs e)
        {
            nameTb.Text = "";
            countryCb.SelectedIndex = -1;
            cityCb.SelectedIndex = -1;
            HouseCheckBox.IsChecked = true;
            ApartmentCheckBox.IsChecked = true;
            CottageCheckBox.IsChecked = true;
            maxGuestsTb.Text = "";
            minReservationDaysTb.Text = "";

            countryCb.SelectedIndex = 3;
            System.Threading.Thread.Sleep(1000);
            cityCb.SelectedIndex = 2;
            System.Threading.Thread.Sleep(1000);
            HouseCheckBox.IsChecked = false;
            System.Threading.Thread.Sleep(1000);
            CottageCheckBox.IsChecked = false;
            System.Threading.Thread.Sleep(1000);
            maxGuestsTb.Text = "2";
            System.Threading.Thread.Sleep(1000);
            minReservationDaysTb.Text = "1";
            System.Threading.Thread.Sleep(300);
            minReservationDaysTb.Text = "10";
            System.Threading.Thread.Sleep(1000);
            nameTb.Text = "H";
            System.Threading.Thread.Sleep(300);
            nameTb.Text = "Ho";
            System.Threading.Thread.Sleep(300);
            nameTb.Text = "Hot";
            System.Threading.Thread.Sleep(300);
            nameTb.Text = "Hote";
            System.Threading.Thread.Sleep(300);
        }
    }
}
