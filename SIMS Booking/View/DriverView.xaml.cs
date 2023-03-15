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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
            foreach (Vehicle vehicle in Vehicles)
                Trace.WriteLine(vehicle.Locations);
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
