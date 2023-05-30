using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class LocationPopularityViewModel : ViewModelBase
    {
        private readonly ReservationService _reservationService;

        public ObservableCollection<Location> PopularAccommodationLocations { get; set; }
        public Location SelectedPopularAccommodationLocation { get; set; }
        public ObservableCollection<Location> UnpopularAccommodationLocations { get; set; }
        public Location SelectedUnpopularAccommodationLocation { get; set; }

        public ICommand NavigateBackCommand { get; }

        public LocationPopularityViewModel(ReservationService reservationService, ModalNavigationStore modalNavigationStore) 
        {
            _reservationService = reservationService;

            PopularAccommodationLocations = new ObservableCollection<Location>(_reservationService.GetMostPopularLocations());
            UnpopularAccommodationLocations = new ObservableCollection<Location>(_reservationService.GetLeastPopularLocations());

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
