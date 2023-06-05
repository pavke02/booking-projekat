using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class PublishAccommodationOnLocationViewModel : ViewModelBase, IDataErrorInfo, IPublish
    {
        private readonly AccommodationService _accommodationService;
        private readonly UsersAccommodationService _usersAccommodationService;

        public ICommand NavigateBackCommand { get; }
        public ICommand PublishCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand ClearURLsCommand { get; }

        #region Property
        public List<string> TypesCollection { get; set; }

        private bool _buttCancel;
        public bool ButtCancel
        {
            get => _buttCancel;
            set
            {
                if (value != _buttCancel)
                {
                    _buttCancel = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string _cancellationPeriod;
        public string CancellationPeriod
        {
            get => _cancellationPeriod;
            set
            {
                if (value != _cancellationPeriod)
                {
                    _cancellationPeriod = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
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
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public PublishAccommodationOnLocationViewModel(AccommodationService accommodationService, UsersAccommodationService usersAccommodationService, 
            User user, Location popularLocation, ModalNavigationStore modalNavigationStore)
        {
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;

            City = popularLocation.City;
            Country = popularLocation.Country;

            PublishCommand = new PublishAccommodationCommand(this, _accommodationService, _usersAccommodationService, user);
            ResetCommand = new ResetCommand(this);
            AddImageCommand = new AddImageCommand(this);
            ClearURLsCommand = new ClearURLsCommand(this);

            TypesCollection = new List<string> { "Apartment", "House", "Cottage" };

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        #region Validation
        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "AccommodationName":
                        if (string.IsNullOrEmpty(AccommodationName))
                            result = "Accommodation name must not be empty";
                        break;
                    case "MaxGuests":
                        if (string.IsNullOrEmpty(MaxGuests))
                            result = "Max guests must not be empty";
                        else if (!int.TryParse(MaxGuests, out _))
                            result = "Max guests must be number";
                        break;
                    case "MinReservationDays":
                        if (string.IsNullOrEmpty(MinReservationDays))
                            result = "Min reservation days must not be empty";
                        else if (!int.TryParse(MinReservationDays, out _))
                            result = "Min reservation days must be number";
                        break;
                    case "CancellationPeriod":
                        if (string.IsNullOrEmpty(CancellationPeriod))
                            result = "Cancellation period must not be empty";
                        else if (!int.TryParse(CancellationPeriod, out _))
                            result = "Cancellation period must be number";
                        break;
                    case "ImageURLs":
                        if (string.IsNullOrEmpty(ImageURLs))
                            result = "You must add at least one image URL";
                        break;
                    case "AccommodationType":
                        if (string.IsNullOrEmpty(AccommodationType))
                            result = "Accommodation type can not be empty";
                        break;
                }

                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = result;
                else if (result != null)
                    ErrorCollection.Add(columnName, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        #endregion
    }
}
