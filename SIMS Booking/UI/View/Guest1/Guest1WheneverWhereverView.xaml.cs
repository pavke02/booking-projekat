using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_Booking.UI.View.Guest1
{
    /// <summary>
    /// Interaction logic for Guest1WheneverWhereverView.xaml
    /// </summary>
    public partial class Guest1WheneverWhereverView : UserControl
    {
        public Guest1WheneverWhereverView()
        {
            InitializeComponent();
            StartDateDp.SelectedDate = DateTime.Today;
            EndDateDp.SelectedDate = DateTime.Today.AddDays(1);
            StartDateDp.DisplayDateStart = DateTime.Today;
            EndDateDp.DisplayDateStart = DateTime.Today.AddDays(1);
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

        private void StartDateChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (StartDateDp.SelectedDate != null)
            {
                EndDateDp.SelectedDate = StartDateDp.SelectedDate.Value.AddDays(1);
                EndDateDp.DisplayDateStart = StartDateDp.SelectedDate.Value.AddDays(1);

            }
        }
    }
}
