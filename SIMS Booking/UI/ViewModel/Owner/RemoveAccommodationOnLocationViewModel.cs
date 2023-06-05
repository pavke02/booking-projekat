using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Commands.OwnerCommands;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class RemoveAccommodationOnLocationViewModel : ViewModelBase
    {
        private AccommodationService _accommodationService;
        private UsersAccommodationService _usersAccommodationService;

        public ObservableCollection<Accommodation> UnpopularAccommodations { get; set; }

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand RemoveAccommodationCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public RemoveAccommodationOnLocationViewModel(AccommodationService accommodationService, UsersAccommodationService usersAccommodationService, 
            Location unpopularLocation, ModalNavigationStore modalNavigationStore)
        {
            _accommodationService = accommodationService;
            _usersAccommodationService = usersAccommodationService;

            UnpopularAccommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAccommodationsByLocation(unpopularLocation));

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
            RemoveAccommodationCommand = new RemoveAccommodationCommand(_accommodationService, _usersAccommodationService, this);
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
