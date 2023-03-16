using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using System.Diagnostics;

namespace SIMS_Booking.View
{
    public partial class DriverView : Window, IObserver
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<string> DriverLocations { get; set; }
        private VehicleRepository _vehicleRepository;
        private CityCountryRepository _cityCountryRepository;
        private DriverLanguagesRepository _driverLanguagesRepository;
        private DriverLocationsRepository _driverLocationsRepository;

        public DriverView(VehicleRepository vehicleRepository, DriverLanguagesRepository driverLanguagesRepository, DriverLocationsRepository driverLocationsRepository, CityCountryRepository cityCountryRepository)
        {
            InitializeComponent();

            _cityCountryRepository = cityCountryRepository;

            _vehicleRepository = vehicleRepository;
            _vehicleRepository.Subscribe(this);

            _driverLanguagesRepository = driverLanguagesRepository;
            _driverLocationsRepository = driverLocationsRepository;

            Vehicles = new ObservableCollection<Vehicle>(vehicleRepository.GetAll());
            DriverLocations = ShowLocations(Vehicles);

            DataGridVehicles.ItemsSource = Vehicles;
        }

        public ObservableCollection<string> ShowLocations(ObservableCollection<Vehicle> vehicles)
        {
            ObservableCollection<string> vehicleLocations = new ObservableCollection<string>();

            foreach(Vehicle vehicle in vehicles)
            {
                string oneLocation = "";
                foreach(Location location in vehicle.Locations)
                {
                    oneLocation += location.ToString() + " ";
                }
                vehicleLocations.Add(oneLocation);
            }


            return vehicleLocations;
        }

        private void UpdateVehicles(List<Vehicle> vehicles)
        {
            Vehicles.Clear();
            foreach (var vehicle in vehicles)
                Vehicles.Add(vehicle);
        }

        public void Update()
        {
            UpdateVehicles(_vehicleRepository.GetAll());
        }

        private void AddVehicle(object sender, RoutedEventArgs e)
        {
            DriverAddVehicle driverAddVehicle = new DriverAddVehicle();
            driverAddVehicle.Show();
        }
    }
}
