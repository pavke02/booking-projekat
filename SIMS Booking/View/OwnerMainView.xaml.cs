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
using System.Windows.Controls;
using SIMS_Booking.Observer;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SIMS_Booking.View
{
    public partial class OwnerMainView : Window, IObserver
    {
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }      
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        private AccomodationRepository _accommodationRepository;
        private CityCountryRepository _cityCountryRepository;
        private ReservationRepository _reservationRepository;

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

        private string _imageURLs;
        public string ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (value != _imageURLs)
                {
                    _imageURLs = value;                                                         
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public OwnerMainView(AccomodationRepository accomodationRepository, CityCountryRepository cityCountryRepository, ReservationRepository reservationRepository)
        {
            InitializeComponent();
            DataContext = this;                       

            _accommodationRepository = accomodationRepository;
            _accommodationRepository.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());

            _reservationRepository = reservationRepository;
            _reservationRepository.Subscribe(this);            
            ReservedAccommodations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());

            _cityCountryRepository = cityCountryRepository;
            Countries = new Dictionary<string, List<string>>(_cityCountryRepository.GetAll());

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };            
        }

        private void ChangeCities(object sender, SelectionChangedEventArgs e)
        {
            cityCb.Items.Clear();

            if(countryCb.SelectedIndex != -1) 
            {
                foreach (string city in Countries.ElementAt(countryCb.SelectedIndex).Value)
                    cityCb.Items.Add(city).ToString();
            }                        
        }


        private void Publish(object sender, RoutedEventArgs e)
        {
            Location location = new Location(Country.Key, City);

            List<string> imageURLs = new List<string>();
            string[] values = ImageURLs.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);

            Accommodation accommodation = new Accommodation(AccommodationName, location, (Kind)Enum.Parse(typeof(Kind), Kind), int.Parse(MaxGuests), int.Parse(MinReservationDays), int.Parse(CancelationPeriod), imageURLs);
            _accommodationRepository.Save(accommodation);
            MessageBox.Show("Accommodation published successfully");
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            accommodationNameTb.Clear();
            maxGuestsTb.Clear();
            minReservationDaysTb.Clear();
            cancelationPeriodTb.Clear();
            countryCb.SelectedItem = null;
            cityCb.SelectedItem = null;
            typeCb.SelectedItem = null;
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();            

            bool? response = openFileDialog.ShowDialog();

            if(response == true)
            {
                string filePath = openFileDialog.FileName;                
                if(imageTb.Text == "")
                {
                    imageTb.Text = filePath;
                    ImageURLs = imageTb.Text;
                }                    
                else
                {
                    imageTb.Text = imageTb.Text + "\n" + filePath;
                    ImageURLs = imageTb.Text;
                }                    
            }
        }

        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach(var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationRepository.GetAll());
        }        
    }
}
