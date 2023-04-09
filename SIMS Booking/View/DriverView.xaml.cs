using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMS_Booking.Enums;
using System.Windows.Controls;
using SIMS_Booking.Service;

namespace SIMS_Booking.View
{
    public partial class DriverView : Window , INotifyPropertyChanged, IObserver
    {
        public Vehicle Vehicle { get; set; }
        public User User { get; set; }


        private RidesRepository _ridesRepository;
        private FinishedRidesRepository _finishedRidesRepository;
        private VehicleService _vehicleService;
        private VehicleCsvCrudRepository _vehicleCsvCrudRepository;
        private CityCountryCsvRepository _cityCountryCsvRepository;
        private DriverLanguagesCsvCrudRepository _driverLanguagesCsvCrudRepository;
        private DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRepository;

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



        public DriverView(User user, RidesRepository ridesRepository, FinishedRidesRepository finishedRidesRepository, VehicleCsvCrudRepository vehicleCsvCrudRepository, VehicleService vehicleService, DriverLanguagesCsvCrudRepository driverLanguagesCsvCrudRepository, DriverLocationsCsvCrudRepository driverLocationsCsvCrudRepository, CityCountryCsvRepository cityCountryCsvRepository)

        {
            InitializeComponent();
            DataContext = this;

            User = user;

            _cityCountryCsvRepository = cityCountryCsvRepository;


            _vehicleService = vehicleService;

            _vehicleCsvCrudRepository = vehicleCsvCrudRepository;
            _vehicleCsvCrudRepository.Subscribe(this);


            _driverLanguagesCsvCrudRepository = driverLanguagesCsvCrudRepository;
            _driverLocationsCsvCrudRepository = driverLocationsCsvCrudRepository;

            Languages = new List<Language>();
            Locations = new List<Location>();

            Vehicle = _vehicleCsvCrudRepository.GetVehicleByUserID(User.getID());

            _ridesRepository = ridesRepository;
            _finishedRidesRepository = finishedRidesRepository;


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
            DriverAddVehicle driverAddVehicle = new DriverAddVehicle(_vehicleCsvCrudRepository, _driverLanguagesCsvCrudRepository, _driverLocationsCsvCrudRepository, _cityCountryCsvRepository, User);
            driverAddVehicle.Show();
        }

        public void Update()
        {
            Vehicle = _vehicleCsvCrudRepository.GetVehicleByUserID(User.getID());

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
            }
            else
            {
                AddVehicle.IsEnabled = false;
                DriverGallery.IsEnabled = true;
            }
        }

        private void DriverGallery_Click(object sender, RoutedEventArgs e)
        {
            DriverGalleryView galleryView = new DriverGalleryView(Vehicle);
            galleryView.Show();
        }

        private void ViewRides_Click(object sender, RoutedEventArgs e)
        {
            DriverRides driverRides = new DriverRides(User, _ridesRepository, _finishedRidesRepository);
            driverRides.Show();
        }

        private void ViewStatsButton_Click(object sender, RoutedEventArgs e)
        {
            DriverStatsView driverStatsView = new DriverStatsView(_finishedRidesRepository);
            driverStatsView.Show();
        }
    }
}
