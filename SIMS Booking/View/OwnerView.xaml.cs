using Microsoft.Win32;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMS_Booking.View
{    
    public partial class OwnerView : Window
    {
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        private AccomodationRepository _accommodationRepository;
        private CityCountryRepository _cityCountryRepository;        

        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
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

        private KeyValuePair<string, List<string>> _country;
        public KeyValuePair<string, List<string>> Country
        {
            get => _country;
            set
            {
                if (value.Key != _country.Key)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _kind;
        public string Kind
        {
            get => _kind;
            set
            {
                if (value != _kind)
                {
                    _kind = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
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

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _cancelationPeriod;
        public string CancelationPeriod
        {
            get => _cancelationPeriod;
            set
            {
                if (value != _cancelationPeriod)
                {
                    _cancelationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public OwnerView(AccomodationRepository accomodationRepository, CityCountryRepository cityCountryRepository)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationRepository = accomodationRepository;
            _cityCountryRepository = cityCountryRepository;

            Accommodations = new List<Accommodation>(_accommodationRepository.Load());

            Countries = new Dictionary<string, List<string>>(_cityCountryRepository.GetAll());
                      
            TypesCollection = new List<string>
            {
                "Apartment",
                "House",
                "Cottage"
            };
        }      

        private void ChangeCities(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CityCb.Items.Clear();            
            foreach(string city in Countries.ElementAt(CountryCb.SelectedIndex).Value)
            {                
                CityCb.Items.Add(city).ToString();
            }                        
        }

        private void UploadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.jpg;*.png";            
            openFileDialog.Multiselect = true;               
        }        

        private void Publish(object sender, RoutedEventArgs e)
        {
            Location location = new Location(Country.Key, City);            
            Accommodation accommodation = new Accommodation(AccommodationName, location, (Kind)Enum.Parse(typeof(Kind), Kind), int.Parse(MaxGuests), int.Parse(MinReservationDays), int.Parse(CancelationPeriod), new List<string>() { "fdgfd", "gdfgf"});
            _accommodationRepository.Save(accommodation);
            MessageBox.Show("Accommodation successfully published");
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

        }   
    }
}