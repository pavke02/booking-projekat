using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Commands.NavigateCommands;
using System.Windows.Input;
using SIMS_Booking.Utility.Stores;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Driver
{

    public class DriverViewModel : ViewModelBase, IObserver
    { 
        public User User { get; set; }

        private VehicleService _vehicleService;
        private RidesService _ridesService;
        private FinishedRidesService _finishedRidesService;
        private DriverLocationsService _driverLocationsService;
        private DriverLanguagesService _driverLanguagesService;
        private CityCountryCsvRepository _cityCountryCsvRepository;

        public ICommand NavigateToGalleryViewCommand { get; }
        public ICommand NavigateToStatsViewCommand { get; }
        public ICommand NavigateToRidesCommand { get; }
        public ICommand NavigateToAddVehicleCommand { get; }
        public ICommand NavigateToProfileCommand { get; }
        public ICommand NavigateToRequestVacationCommand { get; }
        public ICommand NavigateToTutorialCommand { get; }


        private Vehicle _vehicle;
        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {
                if (value != _vehicle)
                {
                    _vehicle = value;
                    OnPropertyChanged();
                }
            }
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
        private string _locationsString;
        public string LocationsString
        {
            get => _locationsString;
            set
            {
                _locationsString = value;
                OnPropertyChanged();
            }
        }

        private string _languagesString;
        public string LanguagesString
        {
            get => _languagesString;
            set
            {
                _languagesString = value;
                OnPropertyChanged();
            }
        }

        private string _maxGuestsString;
        public string MaxGuestsString
        {
            get => _maxGuestsString;
            set
            {
                _maxGuestsString = value;
                OnPropertyChanged();
            }
        }

        public DriverViewModel(User user, RidesService ridesService, FinishedRidesService finishedRidesService, VehicleService vehicleService, DriverLanguagesService driverLanguagesService, DriverLocationsService driverLocationsService, CityCountryCsvRepository cityCountryCsvRepository, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {

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

            _vehicleService.Subscribe(this);

            Update();

            NavigateToGalleryViewCommand = new NavigateCommand(CreateGalleryViewNavigationService(modalNavigationStore), this, () => Vehicle != null);
            NavigateToStatsViewCommand = new NavigateCommand(CreateStatsViewNavigationService(modalNavigationStore), this, () => Vehicle != null);
            NavigateToRidesCommand = new NavigateCommand(CreateRidesNavigationService(modalNavigationStore), this, () => Vehicle != null);
            NavigateToAddVehicleCommand = new NavigateCommand(CreateAddVehicleNavigationService(modalNavigationStore), this, () => Vehicle == null);
            NavigateToProfileCommand = new NavigateCommand(CreateProfileNavigationService(modalNavigationStore), this, () => Vehicle != null);
            NavigateToRequestVacationCommand = new NavigateCommand(CreateRequestVacationNavigationService(modalNavigationStore), this, () => Vehicle != null);
            NavigateToTutorialCommand = new NavigateCommand(CreateTutorialNavigationService(modalNavigationStore), this, () => Vehicle != null);

            if (Vehicle != null)
            {
                foreach (Rides ride in _ridesService.GetActiveRides(User, Vehicle))
                {
                    if (ride.Type == "Fast")
                    {
                        MessageBox.Show("You have new fast rides!");
                        break;
                    }
                }
                foreach (Rides ride in _ridesService.GetActiveRides(User, Vehicle))
                {
                    if (ride.Type == "Group")
                    {
                        MessageBox.Show("You have new group rides!");
                        break;
                    }
                }
            }
            foreach(Vehicle vehicle in _vehicleService.GetAll())
            {
                if (Vehicle != null && vehicle.UserID != Vehicle.UserID)
                {
                    foreach (Rides ride in _ridesService.GetAll())
                    {
                        if (ride.Pending == true)
                        {
                            MessageBox.Show("Your colleague wants to go on a vacation and asks you to take his rides!");
                            break;
                        }
                    }
                }
            }
        }

        public string LocationsToString(List<Location> locations)
        {
            string AllLocations = "";
            foreach (Location location in locations)
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

                LocationsString = LocationsToString(Locations);
                LanguagesString = LanguagesToString(Languages);
                MaxGuestsString = MaxGuests.ToString();
            }
        }



        private INavigationService CreateGalleryViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverGalleryViewModel>
                (modalNavigationStore, () => new DriverGalleryViewModel(Vehicle, modalNavigationStore));
        }

        private INavigationService CreateStatsViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverStatsViewModel>
                (modalNavigationStore, () => new DriverStatsViewModel(_finishedRidesService, User, modalNavigationStore));
        }

        private INavigationService CreateRidesNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverRidesViewModel>
                (modalNavigationStore, () => new DriverRidesViewModel(User,_ridesService, _finishedRidesService, _vehicleService, modalNavigationStore));
        }

        private INavigationService CreateAddVehicleNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverAddVehicleViewModel>
                (modalNavigationStore, () => new DriverAddVehicleViewModel(_vehicleService, _driverLanguagesService, _driverLocationsService, _cityCountryCsvRepository, User, modalNavigationStore));
        }

        private INavigationService CreateProfileNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverProfileViewModel>
                (modalNavigationStore, () => new DriverProfileViewModel(modalNavigationStore, _finishedRidesService, _vehicleService, _ridesService, User));
        }

        private INavigationService CreateRequestVacationNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverRequestVacationViewModel>
                (modalNavigationStore, () => new DriverRequestVacationViewModel(Vehicle, modalNavigationStore, _vehicleService, _ridesService));
        }
        private INavigationService CreateTutorialNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DriverTutorialViewModel>
                (modalNavigationStore, () => new DriverTutorialViewModel(modalNavigationStore));
        }
    }
}
