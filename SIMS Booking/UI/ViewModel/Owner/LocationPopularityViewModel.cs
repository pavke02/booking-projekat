using SIMS_Booking.Commands.DriverCommands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class LocationPopularityViewModel : ViewModelBase
    {
        private readonly ReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly UsersAccommodationService _usersAccommodationService;

        private readonly User _user;

        public ICommand NavigateBackCommand { get; }
        public ICommand PublishOnLocationCommand { get; }
        public ICommand RemoveOnLocationCommand { get; }

        public ObservableCollection<Location> PopularLocations { get; set; }
        public ObservableCollection<Location> UnpopularLocations { get; set; }

        private Location _selectedPopularLocation;
        public Location SelectedPopularLocation
        {
            get => _selectedPopularLocation;
            set
            {
                if (value != _selectedPopularLocation)
                {
                    _selectedPopularLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        private Location _selectedUnpopularLocation;
        public Location SelectedUnpopularLocation
        {
            get => _selectedUnpopularLocation;
            set
            {
                if (value != _selectedUnpopularLocation)
                {
                    _selectedUnpopularLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public LocationPopularityViewModel(ReservationService reservationService, AccommodationService accommodationService, 
            UsersAccommodationService usersAccommodationService, User user, ModalNavigationStore modalNavigationStore) 
        {
            _reservationService = reservationService;
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;

            _user = user;

            PopularLocations = new ObservableCollection<Location>(_reservationService.GetMostPopularLocations());
            UnpopularLocations = new ObservableCollection<Location>(_reservationService.GetLeastPopularLocations());

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
            PublishOnLocationCommand =
                new NavigateCommand(CreatePublishAccommodationOnLocationService(modalNavigationStore), this,  () => SelectedPopularLocation != null);
            RemoveOnLocationCommand =
                new NavigateCommand(CreateRemoveAccommodationOnLocationNavigationService(modalNavigationStore), this, () => SelectedUnpopularLocation != null);
        }

        private INavigationService CreateRemoveAccommodationOnLocationNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<RemoveAccommodationOnLocationViewModel>
                (modalNavigationStore, () => new RemoveAccommodationOnLocationViewModel(_accommodationService, _usersAccommodationService, SelectedUnpopularLocation, modalNavigationStore));
        }

        private INavigationService CreatePublishAccommodationOnLocationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<PublishAccommodationOnLocationViewModel>
                (modalNavigationStore, () => new PublishAccommodationOnLocationViewModel(_accommodationService, _usersAccommodationService, _user, SelectedPopularLocation, modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
