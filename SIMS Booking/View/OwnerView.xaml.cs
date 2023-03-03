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

        private AccomodationRepository _accommodationRepository;

        private string accommodationName;
        public string AccommodationName
        {
            get => accommodationName;
            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string city;
        public string City
        {
            get => city;
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private KeyValuePair<string, List<string>> country;
        public KeyValuePair<string, List<string>> Country
        {
            get => country;
            set
            {
                if (value.Key != country.Key)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string kind;
        public string Kind
        {
            get => kind;
            set
            {
                if (value != kind)
                {
                    kind = value;
                    OnPropertyChanged();
                }
            }
        }

        private string maxGuests;
        public string MaxGuests
        {
            get => maxGuests;
            set
            {
                if (value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private string minReservationDays;
        public string MinReservationDays
        {
            get => minReservationDays;
            set
            {
                if (value != minReservationDays)
                {
                    minReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }

        private string cancelationPeriod;
        public string CancelationPeriod
        {
            get => cancelationPeriod;
            set
            {
                if (value != cancelationPeriod)
                {
                    cancelationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }        

        public OwnerView(AccomodationRepository accomodationRepository)
        {
            InitializeComponent();
            DataContext = this;

            _accommodationRepository = accomodationRepository;

            Countries = new Dictionary<string, List<string>>()
            {
                {"Austria", new List<string>(){ "Graz", "Salzburg", "Vienna" } },
                {"England", new List<string>(){"Birmingham", "London", "Manchester"} },
                {"France", new List<string>(){"Bordeaux", "Marseille", "Paris" } },
                {"Germany", new List<string>(){"Berlin", "Frankfurt", "Mainz" } },
                {"Italy", new List<string>(){"Milano", "Roma", "Venice"} },
                {"Serbia", new List<string>(){"Belgrade", "Nis", "Novi Sad" } },
                {"Spain", new List<string>(){"Barcelona", "Madrid", "Malaga"} }
            };            
            
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
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {

        }   
    }
}