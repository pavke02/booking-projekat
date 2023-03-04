using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Enums;


namespace SIMS_Booking.View
{
    public partial class Guest1FilterView : Window
    {
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        public AccomodationRepository _accommodationRepository { get; set; }
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

        private Kind _kind;
        public Kind Kind
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
         
        public Guest1FilterView(AccomodationRepository accomodationRepository, CityCountryRepository cityCountryRepository)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationRepository = accomodationRepository;
            _cityCountryRepository = cityCountryRepository; 

            Accommodations = _accommodationRepository.Load();
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
            if (CountryCb.SelectedIndex != -1)
            {
                foreach (string city in Countries.ElementAt(CountryCb.SelectedIndex).Value)
                {
                    CityCb.Items.Add(city).ToString();
                }
            }
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

            NameTb.Clear();
            AccommodationName = "";
            CountryCb.SelectedItem = null;
            Country = new KeyValuePair<string, List<string>>();
            CityCb.SelectedItem = null;
            City = "";
            TypeCb.SelectedItem = null;
            Kind = Kind.NoKind;
            MaxGuestsTb.Clear();
            MaxGuests = "0";
            MinReservationDaysTb.Clear();
            MinReservationDays = "10";
            

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = _accommodationRepository.Load();

                }
            }

        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
           // Repository = new AccomodationRepository();
            List<Accommodation> accommodationsFiltered = _accommodationRepository.Load();
            int numberOfDeleted = 0;
            foreach (Accommodation accommodation in Accommodations)
            {
                if (!accommodation.Name.ToLower().Contains(AccommodationName.ToLower()))
                { 
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
                else if (Country.Key != accommodation.Location.Country && Country.Key != null)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
                else if (accommodation.Location.City != City && CityCb.SelectedIndex != -1)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
                else if (accommodation.Type != Kind && Kind != Kind.NoKind)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
                else if (accommodation.MaxGuests < Convert.ToInt32(MaxGuests) && MaxGuests != null)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
                else if (accommodation.MinReservationDays > Convert.ToInt32(MinReservationDays) && MinReservationDays != null)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = accommodationsFiltered;

                }
            }           
        }
    }
}
