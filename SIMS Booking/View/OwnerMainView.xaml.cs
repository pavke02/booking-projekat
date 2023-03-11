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
using System.Windows.Data;
using System.Globalization;

namespace SIMS_Booking.View
{
    public partial class OwnerMainView : Window, IObserver, INotifyPropertyChanged, IDataErrorInfo
    {
        public Dictionary<string, List<string>> Countries { get; set; }
        public List<string> TypesCollection { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }
        public Reservation SelectedReservedAccommodation { get; set; }

        private AccomodationRepository _accommodationRepository;
        private CityCountryRepository _cityCountryRepository;
        private ReservationRepository _reservationRepository;
        private GuestReviewRepository _guestReviewRepository;

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

        private string _accommodationType;
        public string AccommodationType
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
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

        public OwnerMainView(AccomodationRepository accomodationRepository, CityCountryRepository cityCountryRepository, ReservationRepository reservationRepository, GuestReviewRepository guestReviewRepository)
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
            Countries = new Dictionary<string, List<string>>(_cityCountryRepository.Load());

            _guestReviewRepository = guestReviewRepository;

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };
        }

        //ToDo: Napraviti da se ovo proverava na svakih 10min recimo
        private void CheckDate(object sender, SelectionChangedEventArgs e)
        {
            if (DateTime.Now >= SelectedReservedAccommodation.EndDate && (DateTime.Now - SelectedReservedAccommodation.EndDate.Date).TotalDays < 5)
                reviewGuestClick.IsEnabled = true;
            else
                reviewGuestClick.IsEnabled = false;
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

        //ToDO:
        private void Publish(object sender, RoutedEventArgs e)
        {
            Location location = new Location(Country.Key, City);

            List<string> imageURLs = new List<string>();
            string[] values = ImageURLs.Split("\n");
            foreach (string value in values)
                imageURLs.Add(value);

            Accommodation accommodation = new Accommodation(AccommodationName, location, (AccommodationType)Enum.Parse(typeof(AccommodationType), AccommodationType), int.Parse(MaxGuests), int.Parse(MinReservationDays), int.Parse(CancelationPeriod), imageURLs);
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

            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                if (imageTb.Text == "")
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

        private void RateGuest(object sender, RoutedEventArgs e)
        {
            GuestReviewView guestReviewView = new GuestReviewView(_guestReviewRepository);
            guestReviewView.ShowDialog();
        }

        private void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationRepository.GetAll());
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "AccommodationName")
                {
                    if (string.IsNullOrEmpty(AccommodationName)) 
                        return "Required";
                }                                  
                else if( columnName == "City")
                {
                    if (string.IsNullOrEmpty(City)) 
                        return "Required";
                }                
                else if(columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country.ToString())) 
                        return "Required";
                }
                else if (columnName == "MaxGuests")
                {
                    if (string.IsNullOrEmpty(MaxGuests)) 
                        return "Required";
                }
                else if(columnName == "MinReservationDays")
                {
                    if (string.IsNullOrEmpty(MinReservationDays)) 
                        return "Required";
                }                
                else if(columnName == "CancelationPeriod")
                {
                    if (string.IsNullOrEmpty(CancelationPeriod)) 
                        return "Cancelation period is required";
                }
                else if(columnName == "AccommodationType")
                {
                    if(string.IsNullOrEmpty(AccommodationType)) 
                        return "Required";
                }
                return null;
            }
        }

        private void TextBoxCheck(object sender, RoutedEventArgs e)
        {
            publishButton.IsEnabled = false;
            if(IsValid)
            {
                publishButton.IsEnabled = true;
            }
        }

        private readonly string[] validatedProperties = { "AccommodationName", "City", "Country", "MaxGuests", "MinReservationDays", "CancelationPeriod", "AccommodationType" };

        public bool IsValid
        {
            get
            {
                foreach (var property in validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }        
    }

    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return DateTime.Now >= (DateTime)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotImplementedException();
        }
    }    
}
