// using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SIMS_Booking.UI.View.Guide
{
    public partial class CreateTour : UserControl
    {
        public CreateTour()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
                    
            if (!int.TryParse(textBox.Text, out int result))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;
            
        }

        private void TextBox_LostFocusINT(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if (int.TryParse(textBox.Text, out int result))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;

        }
    }
}
