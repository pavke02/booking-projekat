using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_Booking.View
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
