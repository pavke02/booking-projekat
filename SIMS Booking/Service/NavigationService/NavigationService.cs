using SIMS_Booking.Utility.Stores;
using System;
using SIMS_Booking.UI.ViewModel;

namespace SIMS_Booking.Service.NavigationService
{
    public class NavigationService<TViewModel> where TViewModel : ViewModelBase
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
