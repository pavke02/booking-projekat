using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.View
{
    /// <summary>
    /// Interaction logic for Guest2TourReservation.xaml
    /// </summary>
    public partial class Guest2TourReservation : Window, IObserver
    {

        private readonly Tour _selectedTour;
        public Voucher SelectedVoucher { get; set; }
        public User LoggedUser { get; set; }
        
        private ReservedTourService _reservedTourService;
        public TourReservation _tourReservation;
        public List<Reservation> Reservations { get; set; } 
        public List<Reservation> TourReservations { get; set; }
        private VoucherService _voucherService;

        public ObservableCollection<Voucher> Vouchers { get; set; }


                                          


        private int maxGuests;
        private string name;


        public Guest2TourReservation(Tour selectedTour, User loggedUser, ReservedTourService reservedTourService, VoucherService voucherService, Voucher voucher)
        {
            InitializeComponent();


            _reservedTourService = reservedTourService;
            _voucherService = voucherService;

            DataContext = this;   

            LoggedUser = loggedUser;
            _selectedTour = selectedTour;
            SelectedVoucher = voucher;

            BoxName.Text = selectedTour.Name;
            BoxLocation.Text = selectedTour.Location.City;
            BoxDescription.Text = selectedTour.Description;
            BoxLanguage.Text = selectedTour.Language.ToString();
            BoxMaxGuests.Text = selectedTour.MaxGuests.ToString();
            BoxTime.Text = selectedTour.Time.ToString();
            AvailableNumber.Text = (selectedTour.MaxGuests - _reservedTourService.GetNumberOfGuestsForTour(selectedTour.GetId())).ToString();


            BoxName.IsReadOnly = true;
            BoxLocation.IsReadOnly = true;
            BoxDescription.IsReadOnly = true;
            BoxLanguage.IsReadOnly = true;
            BoxMaxGuests.IsReadOnly = true;
            BoxTime.IsReadOnly = true;
            AvailableNumber.IsReadOnly = true;

            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAllValidVouchers());
        }



        private void Confirm(object sender, RoutedEventArgs e)
        {
            int numberOfGuests;

            if (!int.TryParse(NumberOfGuests.Text, out numberOfGuests)) {

                MessageBox.Show("Niste uneli u dobrom formatu.");

            }
            else if (Convert.ToInt32(NumberOfGuests.Text) == null)
            {
                MessageBox.Show("Molim Vas da unesete broj gostiju.");

            }
            else if (_selectedTour.MaxGuests < Convert.ToInt32(NumberOfGuests.Text) + _reservedTourService.GetNumberOfGuestsForTour(_selectedTour.GetId()))
            {
                MessageBox.Show($"Number of guests cannot be more than the maximum number of guests for this tour ({_selectedTour.MaxGuests - _reservedTourService.GetNumberOfGuestsForTour(_selectedTour.GetId())} guests).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
            else if (_selectedTour.MaxGuests >= Convert.ToInt32(NumberOfGuests.Text))
            {
                if (DataGridVouchers.SelectedIndex != -1)
                {
                    _voucherService.UseVoucher(SelectedVoucher);
                }

                int _maxGuests;

                _maxGuests = maxGuests - Convert.ToInt32(NumberOfGuests.Text);


                TourReservation tourReservation = new TourReservation(LoggedUser.GetId(), _selectedTour.GetId(), Convert.ToInt32(NumberOfGuests.Text));
            
                _reservedTourService.Save(tourReservation);
                AvailableNumber.Text = (_selectedTour.MaxGuests - _reservedTourService.GetNumberOfGuestsForTour(_selectedTour.GetId())).ToString();

                MessageBox.Show($"You reserved for ({Convert.ToInt32(NumberOfGuests.Text)} guests)");
            }          







        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            GetWindow(this).Close();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
