using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Repository;
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
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2TourReservation.xaml
    /// </summary>
    public partial class Guest2TourReservation : Window
    {

        private readonly Tour _selectedTour;
        public User LoggedUser { get; set; }
        private ReservedToursRepository _reservedToursRepository;
        public TourReservation _tourReservation;
        public List<Reservation> Reservations { get; set; }
        public List<Reservation> TourReservations { get; set; }

        private int maxGuests;
        private string name;

       public Guest2TourReservation(string name, Location location, string description, Language language, int maxGuests, int time, User loggedUser)
        {
            InitializeComponent();

            LoggedUser = loggedUser;
            
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
        {
            if (Convert.ToInt32(NumberOfGuests.Text) == null)
            {
                MessageBox.Show("Please enter the number of guests.");
                
            }
            else if(maxGuests < Convert.ToInt32(NumberOfGuests.Text))
            {
                MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this tour ({maxGuests} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (maxGuests >= Convert.ToInt32(NumberOfGuests.Text))
            {
                int _maxGuests;

                _maxGuests = maxGuests - Convert.ToInt32(NumberOfGuests.Text);


                TourReservation tourReservation = new TourReservation(LoggedUser.getID(), _selectedTour.getID());
                _reservedToursRepository.Save(tourReservation);
                MessageBox.Show($"You reserved for ({Convert.ToInt32(NumberOfGuests.Text)} guests)");

                

            }          
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }
    }
}
