using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMS_Booking.Enums;
using SIMS_Booking.Observer;
using SIMS_Booking.Service;


namespace SIMS_Booking.View
{
    public partial class Guest1FilterView : Window
    {
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public List<Accommodation> Accommodations { get; set; }
        private AccommodationService _accommodationService { get; set; }
        private CityCountryCsvRepository _cityCountryCsvRepository;

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

        private List<AccommodationType> _kinds;

        public List<AccommodationType> Kinds
        {
            get => _kinds;
            set
            {
                if (value != _kinds)
                {
                    _kinds = value;
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
         
        public Guest1FilterView(AccommodationService accommodationService, CityCountryCsvRepository cityCountryCsvRepository)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationService = accommodationService;
            _cityCountryCsvRepository = cityCountryCsvRepository; 

            Accommodations = new List<Accommodation>(_accommodationService.GetAll());
            Countries = new Dictionary<string, List<string>>(_cityCountryCsvRepository.Load());

            AccommodationName = "";

            TypesCollection = new List<string>
            {
                "Apartment",
                "House",
                "Cottage"
            };
        }

        private void ChangeCities(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            cityCb.Items.Clear();
            if (countryCb.SelectedIndex != -1)
            {
                foreach (string city in Countries.ElementAt(countryCb.SelectedIndex).Value)
                {
                    cityCb.Items.Add(city).ToString();
                }
            }
            
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ClearFilterFields();

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = _accommodationService.SortBySuperOwner(_accommodationService.GetAll());
                }
            }
        }

        private void ClearFilterFields()
        {
            nameTb.Clear();
            AccommodationName = "";
            countryCb.SelectedItem = null;
            Country = new KeyValuePair<string, List<string>>();
            cityCb.SelectedItem = null;
            City = "";
            HouseCheckBox.IsChecked = true;
            ApartmentCheckBox.IsChecked = true;
            CottageCheckBox.IsChecked = true;
            MaxGuests = "0";
            maxGuestsTb.Clear();
            MinReservationDays = "10";
            minReservationDaysTb.Clear();
        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
            List<Accommodation> accommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            int numberOfDeleted = 0;

            UpdateKindsState();

            foreach (Accommodation accommodation in Accommodations)
            {
                bool fitsFilter = (accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) || AccommodationName == "") && (Country.Key == accommodation.Location.Country || countryCb.SelectedIndex == -1)
                    && (accommodation.Location.City == City || cityCb.SelectedIndex == -1) && Kinds.Contains(accommodation.Type) && (accommodation.MaxGuests >= Convert.ToInt32(MaxGuests) || MaxGuests == null)
                    && (accommodation.MinReservationDays <= Convert.ToInt32(MinReservationDays) || MinReservationDays == null);

                if (!fitsFilter)
                {
                    accommodationsFiltered.RemoveAt(Accommodations.IndexOf(accommodation) - numberOfDeleted);
                    numberOfDeleted++;
                }
            }

            UpdateAccommodationsDataGrid(_accommodationService.SortBySuperOwner(accommodationsFiltered));
        }

        private void UpdateKindsState()
        {
            Kinds = new List<AccommodationType>();
            Kinds.Clear();

            if (HouseCheckBox.IsChecked == true)
                Kinds.Add(AccommodationType.House);

            if (ApartmentCheckBox.IsChecked == true)
                Kinds.Add(AccommodationType.Apartment);
            
            if (CottageCheckBox.IsChecked == true)
                Kinds.Add(AccommodationType.Cottage);

        }

        private void UpdateAccommodationsDataGrid(List<Accommodation> accommodationsFiltered)
        {
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
