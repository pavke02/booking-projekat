using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2ReservationView.xaml
    /// </summary>
    public partial class Guest2DrivingReservationView : Window
    {
        public readonly Vehicle _selectedVehicle;
        private readonly VehicleRepository _vehicleRepository;
        public List<Location> Locations { get; set; }
        public List<DriverLocations> DrivingReservations { get; set; }
        public User LoggedUser { get; set; }
        public Address StartingAddress { get; set; }
        public Address EndingAddress { get; set; }
        public ReservationOfVehicle ReservationOfVehicle { get; set; }
        public VehicleReservationRepository _vehicleReservationRespository;
        private string searchLocation;
        public ObservableCollection<DriverLocations> drivers { get; set; }
        public DriverLocations _driverLocations;
        
        private  DriverLocationsRepository _driverLocationsRespository;


         public Guest2DrivingReservationView(Vehicle selectedVehicle, User loggedUser, DriverLocations driverLocations, DriverLocationsRepository driverLocationsRespository)
        {
            InitializeComponent();

            DataContext = this;
            _selectedVehicle = selectedVehicle;
            _driverLocations = driverLocations;
            _driverLocationsRespository = driverLocationsRespository;
            LoggedUser = loggedUser;




            Address startingAddress = StartingAddress;
            Address endingAddress = EndingAddress;








        }

        private void Reserve(object sender, RoutedEventArgs e)
        {


            ReservationOfVehicle reservedVehicle =
                 new ReservationOfVehicle(LoggedUser.getID(), _selectedVehicle.getID());
            _vehicleReservationRespository.Save(reservedVehicle);
            Close();

        }

        public string SearchLocation
        {
            get { return searchLocation; }
            set
            {
                searchLocation = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }

        public ObservableCollection<DriverLocations> filteredData
        {
            get
            {
                var result = drivers;

                if (!string.IsNullOrEmpty(searchLocation))
                {
                    result = new ObservableCollection<DriverLocations>(result.Where(a => a.Location.City.ToLower().Contains(searchLocation) || a.Location.Country.ToLower().Contains(searchLocation)));
                }



                return result;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
