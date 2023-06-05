using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.Windows;
using System.ComponentModel;
using SIMS_Booking.UI.ViewModel.Driver;
using System.Windows.Threading;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.Commands.DriverCommands
{
    class PublishCommand : CommandBase
    {
        private readonly DriverAddVehicleViewModel _viewModel;
        private readonly VehicleService _vehicleService;
        private readonly DriverLanguagesService _driverLanguagesService;
        private readonly DriverLocationsService _driverLocationsService;
        private readonly User user;
        private readonly INavigationService _closeModalNavigationService;


        public PublishCommand(DriverAddVehicleViewModel viewModel, VehicleService vehicleService, DriverLanguagesService driverLanguagesService,
            DriverLocationsService driverLocationsService, User user, INavigationService closeModalNavigationService)
        {
            _viewModel = viewModel;
            _vehicleService = vehicleService;
            _driverLanguagesService = driverLanguagesService;
            _driverLocationsService = driverLocationsService;
            this.user = user;
            _closeModalNavigationService = closeModalNavigationService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            List<Language> languages = new List<Language>();
            ReadLanguages(languages);
            List<Location> locations = new List<Location>();
            ReadLocations(locations);
            List<string> imageurls = new List<string>();
            ReadImageURLs(imageurls);
            int maximumGuests = int.Parse(_viewModel.MaxGuests);

            Vehicle vehicle = new Vehicle(locations, maximumGuests, languages, imageurls, user, 0, "no", DateOnly.MaxValue, DateOnly.MaxValue);
            _vehicleService.Save(vehicle);

            foreach (Language language in languages)
            {
                DriverLanguages driverLanguages = new DriverLanguages(vehicle.GetId(), language);
                _driverLanguagesService.Save(driverLanguages);
            }

            foreach (Location location in locations)
            {
                DriverLocations driverLocations = new DriverLocations(vehicle.GetId(), location);
                _driverLocationsService.Save(driverLocations);
            }

            _driverLocationsService.AddDriverLocationsToVehicles(_vehicleService);
            _driverLanguagesService.AddDriverLanguagesToVehicles(_vehicleService);

            MessageBox.Show("Vehicle published successfully!");

            _closeModalNavigationService.Navigate();

            
        }

        public void ReadLanguages(List<Language> languages)
        {
            string languagesText = _viewModel.Languages;
            string[] languageStrings = languagesText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string languageString in languageStrings)
            {
                if (Enum.TryParse(languageString, out Language language))
                {
                    languages.Add(language);
                }
            }
        }

        public void ReadLocations(List<Location> locations)
        {
            string locationsText = _viewModel.Locations;
            string[] locationStrings = locationsText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string locationString in locationStrings)
            {
                string[] countryAndCity = locationString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (countryAndCity.Length == 2)
                {
                    string country = countryAndCity[0].Trim();
                    string city = countryAndCity[1].Trim();
                    Location location = new Location { Country = country, City = city };
                    locations.Add(location);
                }
            }
        }

        public void ReadImageURLs(List<string> imageurls)
        {
            string imagesText = _viewModel.Images;
            string[] imageStrings = imagesText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string imageString in imageStrings)
            {
                imageurls.Add(imageString);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.Locations) && !string.IsNullOrEmpty(_viewModel.Languages) && 
                   !string.IsNullOrEmpty(_viewModel.Images) && !string.IsNullOrEmpty(_viewModel.MaxGuests) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverAddVehicleViewModel.Locations) || e.PropertyName == nameof(DriverAddVehicleViewModel.Languages) ||
                e.PropertyName == nameof(DriverAddVehicleViewModel.Images) || e.PropertyName == nameof(DriverAddVehicleViewModel.MaxGuests))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
