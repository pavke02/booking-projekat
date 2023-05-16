using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Commands.DriverCommands;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverAddVehicleViewModel : ViewModelBase
    {
        private VehicleService _vehicleService;
        private DriverLanguagesService _driverLanguagesService;
        private DriverLocationsService _driverLocationsService;
        private CityCountryCsvRepository _cityCountryCsvRepository;
        public List<string> AllLanguages { get; set; }
        public User User { get; set; }

        public List<string> Countries { get; set; }
        //public ObservableCollection<string> Cities { get; set; }

        

        //public string Locations { get; set; }
        //public ObservableCollection<string> Languages { get; set; }
        //public ObservableCollection<string> Images { get; set; }
        //public int MaxGuests  { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand AddLocationCommand { get; }
        public ICommand AddLanguageCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand PublishVehicleCommand { get; }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get => _cities;
            set
            {
                if (value != _cities)
                {
                    _cities = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                    FillCityCb();
                }
            }
        }

        private string _locations;
        public string Locations
        {
            get { return _locations; }
            set
            {
                if (_locations != value)
                {
                    _locations = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _languages;
        public string Languages
        {
            get { return _languages; }
            set
            {
                if (_languages != value)
                {
                    _languages = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _images;
        public string Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get { return _maxGuests; }
            set
            {
                if (_maxGuests != value)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (_selectedLanguage != value)
                {
                    _selectedLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        public DriverAddVehicleViewModel(VehicleService vehicleService, DriverLanguagesService driverLanguagesService, DriverLocationsService driverLocationsService, CityCountryCsvRepository cityCountryCsvRepository, User user, ModalNavigationStore modalNavigationStore)
        {
            _vehicleService = vehicleService;
            _driverLanguagesService = driverLanguagesService;
            _driverLocationsService = driverLocationsService;

            _cityCountryCsvRepository = cityCountryCsvRepository;

            Locations = "";
            Languages = "";
            Images = "";

            Countries = new List<string>(_cityCountryCsvRepository.LoadCountries());

            Cities = new ObservableCollection<string>();
            AllLanguages = new List<string> { "Serbian", "English" };

            User = user;

            NavigateBackCommand = new NavigateBackCommand(CreateCloseAddVehicleNavigationService(modalNavigationStore));
            AddLocationCommand = new AddLocation(this);
            AddLanguageCommand = new AddLanguage(this);
            AddImageCommand = new AddImage(this);
            PublishVehicleCommand = new PublishCommand(this, _vehicleService, _driverLanguagesService, _driverLocationsService, User, CreateCloseAddVehicleNavigationService(modalNavigationStore));
        }

        private void FillCityCb()
        {
            Cities.Clear();
            if (Country != null)
                Cities = _cityCountryCsvRepository.LoadCitiesForCountry(Country);
        }

        private INavigationService CreateCloseAddVehicleNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
