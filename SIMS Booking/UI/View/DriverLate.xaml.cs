using System.Windows;

namespace SIMS_Booking.UI.View
{
    public partial class DriverLate : Window
    {

        public int LateInMinutes { get; set; }

        public DriverLate()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void lateButton_Click(object sender, RoutedEventArgs e)
        {
            LateInMinutes = int.Parse(lateTB.Text);
            this.Close();
        }

    }
}
