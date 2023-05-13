using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.View
{
    public partial class DriverView : Window , INotifyPropertyChanged, IObserver
    {
        public Vehicle Vehicle { get; set; }
        public User User { get; set; }

        private VehicleService _vehicleService;
        private RidesService _ridesService;
        private FinishedRidesService _finishedRidesService;
        private DriverLocationsService _driverLocationsService;
        private DriverLanguagesService _driverLanguagesService;
        private CityCountryCsvRepository _cityCountryCsvRepository;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<Location> _locations;
        public List<Location> Locations
        {
            get => _locations;
            set
            {
                if (value != _locations)
                {
                    _locations = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<Language> _languages;
        public List<Language> Languages
        {
            get => _languages;
            set
            {
                if (value != _languages)
                {
                    _languages = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }



        public DriverView(User user, RidesService ridesService, FinishedRidesService finishedRidesService, VehicleService vehicleService, DriverLanguagesService driverLanguagesService, DriverLocationsService driverLocationsService, CityCountryCsvRepository cityCountryCsvRepository)
        {
            InitializeComponent();
            DataContext = this;

            User = user;

            _cityCountryCsvRepository = cityCountryCsvRepository;

            _vehicleService = vehicleService;
            _driverLocationsService = driverLocationsService;
            _driverLanguagesService = driverLanguagesService;

            Languages = new List<Language>();
            Locations = new List<Location>();

            Vehicle = _vehicleService.GetVehicleByUserID(User.GetId());

            _ridesService = ridesService;
            _finishedRidesService = finishedRidesService;

            Update();
        }


        public string LocationsToString(List<Location> locations)
        {
            string AllLocations = "";
            foreach(Location location in locations)
            {
                AllLocations += location.Country.ToString() + ", " + location.City.ToString() + "\n";
            }
            return AllLocations;
        }

        public string LanguagesToString(List<Language> languages)
        {
            string AllLanguages = "";
            foreach (Language language in Languages)
            {
                AllLanguages += language.ToString() + "\n";
            }
            return AllLanguages;
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            DriverAddVehicle driverAddVehicle = new DriverAddVehicle(_vehicleService, _driverLanguagesService, _driverLocationsService, _cityCountryCsvRepository, User);
            driverAddVehicle.Show();
            Update();
        }

        public void Update()
        {

            Vehicle = _vehicleService.GetVehicleByUserID(User.GetId());

            if (Vehicle != null)
            {
                foreach (Language language in Vehicle.Languages)
                {
                    Languages.Add(language);
                }
                foreach (Location location in Vehicle.Locations)
                {
                    Locations.Add(location);
                }
                MaxGuests = Vehicle.MaxGuests;

                LocationsTB.Text = LocationsToString(Locations);
                LanguagesTB.Text = LanguagesToString(Languages);
                MaxGuestsTB.Text = MaxGuests.ToString();
            }

            if (string.IsNullOrEmpty(MaxGuestsTB.Text))
            {
                AddVehicle.IsEnabled = true;
                DriverGallery.IsEnabled = false;
                ViewStatsButton.IsEnabled = false;
                ViewRides.IsEnabled = false;
            }
            else
            {
                AddVehicle.IsEnabled = false;
                DriverGallery.IsEnabled = true;
                ViewStatsButton.IsEnabled = true;
                ViewRides.IsEnabled = true;
            }
        }

        private void DriverGallery_Click(object sender, RoutedEventArgs e)
        {
            DriverGalleryView galleryView = new DriverGalleryView(Vehicle);
            galleryView.Show();
        }

        private void ViewRides_Click(object sender, RoutedEventArgs e)
        {
            DriverRides driverRides = new DriverRides(User, _ridesService, _finishedRidesService, _vehicleService);
            driverRides.Show();
        }

        private void ViewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            DriverStatsView driverStatsView = new DriverStatsView(_finishedRidesService, User);
            driverStatsView.Show();
        }
    }
}
