using SIMS_Booking.Enums;
using SIMS_Booking.Model;
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
using System.Xml.Linq;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2TourReservation.xaml
    /// </summary>
    public partial class Guest2TourReservation : Window
    {
        private int maxGuests;
        private string name;
      

        public Guest2TourReservation(string name, Location location , string description, Language language, int maxGuests, int time)
        {
            InitializeComponent();
            
            this.maxGuests = maxGuests;
            this.name = name;

            BoxName.Text = name;
            BoxLocation.Text = location.City;            
            BoxDescription.Text = description;
            BoxLanguage.Text = language.ToString();
            BoxMaxGuests.Text = maxGuests.ToString();
            BoxTime.Text = time.ToString();

            BoxName.IsReadOnly = true;
            BoxLocation.IsReadOnly = true;
            BoxDescription.IsReadOnly = true;
            BoxLanguage.IsReadOnly = true;
            BoxMaxGuests.IsReadOnly = true;
            BoxTime.IsReadOnly = true;



        }

        

        private void Confirm(object sender, RoutedEventArgs e)
        {/*
            if (NumberOfGuests == null)
            {
                MessageBox.Show("Please enter the number of guests!");
            }else if (NumberOfGuests.GetValue() > maxGuests)
            {




            }
           
            */

        }

            private void Cancel(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }

    }
}
