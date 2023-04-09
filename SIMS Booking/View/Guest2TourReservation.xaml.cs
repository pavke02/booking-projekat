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
        private ReservedToursCsvCrudRepository _reservedToursCsvCrudRepository;
        public TourReservation _tourReservation;
        public List<Reservation> Reservations { get; set; }
        public List<Reservation> TourReservations { get; set; }

        private int maxGuests;
        private string name;

        //string name, Location location, string description, Language language, int maxGuests, int time, User loggedUser

        public Guest2TourReservation(Tour selectedTour, User loggedUser)
        {
            InitializeComponent();

            _reservedToursCsvCrudRepository = new ReservedToursCsvCrudRepository();

            LoggedUser = loggedUser;
            _selectedTour = selectedTour;

            BoxName.Text = selectedTour.Name;
            BoxLocation.Text = selectedTour.Location.City;
            BoxDescription.Text = selectedTour.Description;
            BoxLanguage.Text = selectedTour.Language.ToString();
            BoxMaxGuests.Text = selectedTour.MaxGuests.ToString();
            BoxTime.Text = selectedTour.Time.ToString();
            AvailableNumber.Text = (selectedTour.MaxGuests - _reservedToursCsvCrudRepository.GetNumberOfGuestsForTour(selectedTour.getID())).ToString();


            BoxName.IsReadOnly = true;
            BoxLocation.IsReadOnly = true;
            BoxDescription.IsReadOnly = true;
            BoxLanguage.IsReadOnly = true;
            BoxMaxGuests.IsReadOnly = true;
            BoxTime.IsReadOnly = true;
            AvailableNumber.IsReadOnly = true;
        }



        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(NumberOfGuests.Text) == null)
            {
                MessageBox.Show("Please enter the number of guests.");

            }
            else if (_selectedTour.MaxGuests < Convert.ToInt32(NumberOfGuests.Text) + _reservedToursCsvCrudRepository.GetNumberOfGuestsForTour(_selectedTour.getID()))
            {
                MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this tour ({_selectedTour.MaxGuests - _reservedToursCsvCrudRepository.GetNumberOfGuestsForTour(_selectedTour.getID())} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (_selectedTour.MaxGuests >= Convert.ToInt32(NumberOfGuests.Text))
            {
                int _maxGuests;

                _maxGuests = maxGuests - Convert.ToInt32(NumberOfGuests.Text);


                TourReservation tourReservation = new TourReservation(LoggedUser.getID(), _selectedTour.getID(), Convert.ToInt32(NumberOfGuests.Text));
                _reservedToursCsvCrudRepository.Save(tourReservation);
                AvailableNumber.Text = (_selectedTour.MaxGuests - _reservedToursCsvCrudRepository.GetNumberOfGuestsForTour(_selectedTour.getID())).ToString();

                MessageBox.Show($"You reserved for ({Convert.ToInt32(NumberOfGuests.Text)} guests)");
            }          

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }
    }
}
