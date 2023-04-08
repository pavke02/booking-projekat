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
using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for DriverAddVehicle.xaml
    /// </summary>
    public partial class DriverAddVehicle : Window
    {
        private VehicleCsvCrudRepository _vehicleCsvCrudRepository;
        private DriverLanguagesCsvCrudRepository _driverLanguagesCsvCrudRepository;
        private DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRepository;
        private CityCountryCsvRepository _cityCountryCsvRepository;
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> AllLanguages { get; set; }
        public User User { get; set; }

        public DriverAddVehicle(VehicleCsvCrudRepository vehicleCsvCrudRepository, DriverLanguagesCsvCrudRepository driverLanguagesCsvCrudRepository, DriverLocationsCsvCrudRepository driverLocationsCsvCrudRepository, CityCountryCsvRepository cityCountryCsvRepository, User user)
        {
            InitializeComponent();
            DataContext = this;

            User = user;

            _vehicleCsvCrudRepository = vehicleCsvCrudRepository;
            _driverLanguagesCsvCrudRepository = driverLanguagesCsvCrudRepository;
            _driverLocationsCsvCrudRepository = driverLocationsCsvCrudRepository;
            _cityCountryCsvRepository = cityCountryCsvRepository;
            Countries = new Dictionary<string, List<string>>(_cityCountryCsvRepository.Load());

            AllLanguages = new List<string> { "Serbian", "English" };
        }

        private void ChangeCities(object sender, SelectionChangedEventArgs e)
        {
            cityCb.Items.Clear();

            if (countryCb.SelectedIndex != -1)
            {
                foreach (string city in Countries.ElementAt(countryCb.SelectedIndex).Value)
                    cityCb.Items.Add(city).ToString();
            }
        }

        private void AddLanguage_Click(object sender, RoutedEventArgs e)
        {
            if (languagesTb.Text == "")
                languagesTb.Text = languagesCb.SelectedItem.ToString();
            else
                languagesTb.Text = languagesTb.Text + "\n" + languagesCb.SelectedItem.ToString();

            languagesCb.SelectedItem = null;
        }

        private void AddLocation_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<string, List<string>> selectedCountry = (KeyValuePair<string, List<string>>)countryCb.SelectedItem;
            string selectedCountryKey = selectedCountry.Key;

            if (locationsTb.Text == "")
                locationsTb.Text = selectedCountryKey + ", " + cityCb.SelectedItem.ToString();
            else
                locationsTb.Text = locationsTb.Text + "\n" + selectedCountryKey + ", " + cityCb.SelectedItem.ToString();

            cityCb.SelectedItem = null;
            countryCb.SelectedItem = null;
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            if (imagesTb.Text == "")
                imagesTb.Text = imageURL.Text;
            else
                imagesTb.Text = imagesTb.Text + "\n" + imageURL.Text;

            imageURL.Text = "";
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            List<Language> languages = new List<Language>();
            ReadLanguages(languages);

            List<Location> locations = new List<Location>();
            ReadLocations(locations);

            List<string> imageurls = new List<string>();
            ReadImageURLs(imageurls);

            Vehicle vehicle = new Vehicle(locations, int.Parse(maxGuests.Text), languages, imageurls, User);
            _vehicleCsvCrudRepository.Save(vehicle);

            foreach (Language language in languages)
            {
                DriverLanguages driverLanguages = new DriverLanguages(vehicle.getID(), language);
                _driverLanguagesCsvCrudRepository.Save(driverLanguages);
            }

            foreach (Location location in locations)
            {
                DriverLocations driverLocations = new DriverLocations(vehicle.getID(), location);
                _driverLocationsCsvCrudRepository.Save(driverLocations);
            }

            MessageBox.Show("Vehicle published successfully!");

            Close();
        }

        public void ReadLanguages(List<Language> languages)
        {
            string languagesText = languagesTb.Text;
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
            string locationsText = locationsTb.Text;
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
            string imagesText = imagesTb.Text;
            string[] imageStrings = imagesText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string imageString in imageStrings)
            {
                    imageurls.Add(imageString);
            }
        }
    }
}
