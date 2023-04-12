using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.ViewModel;
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
    /// <summary>
    /// Interaction logic for Guest2FindingTaxiFast.xaml
    /// </summary>
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
