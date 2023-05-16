using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System;

namespace SIMS_Booking.Service.NavigationService
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
