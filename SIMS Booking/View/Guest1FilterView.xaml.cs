using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using SIMS_Booking.Enums;
using SIMS_Booking.View;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest1FilterView.xaml
    /// </summary>
    public partial class Guest1FilterView : Window
    {
        public Dictionary<string, List<string>> Countries { get; set; }

        public List<string> TypesCollection { get; set; }

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

        public List<Accommodation> Accommodations { get; set; }

        public AccomodationRepository Repository { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
         
        public Guest1FilterView()
        {
            InitializeComponent();
            DataContext = this;
            Repository = new AccomodationRepository();
            Accommodations = Repository.Load();

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

            NameTb.Text = "";
            CountryCb.SelectedIndex = -1;
            CityCb.SelectedIndex = -1;
            TypeCb.SelectedIndex = -1;
            MaxGuestsTb.Text = "";
            MinReservationDaysTb.Text = "";
            

            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Guest1MainView))
                {
                    ((window as Guest1MainView)!).DataGridAccommodations.ItemsSource = Repository.Load();

                }
            }

        }

        private void ApplyFilters(object sender, RoutedEventArgs e)
        {
           // Repository = new AccomodationRepository();
            List<Accommodation> accommodationsFiltered = Repository.Load();
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
