using System.Windows;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for PostponeReservationView.xaml
    /// </summary>
    public partial class PostponeReservationView : Window
    {
        public PostponeReservationView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void AcceptPostponmentRequest(object sender, RoutedEventArgs e)
        {

        }

        private void DeclinePostponmentRequest(object sender, RoutedEventArgs e)
        {

        }
    }
}
