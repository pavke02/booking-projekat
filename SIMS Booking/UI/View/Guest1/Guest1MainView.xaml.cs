using System.Text.RegularExpressions;
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
    }
}
