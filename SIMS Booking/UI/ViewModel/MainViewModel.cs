using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set
            {
                _navigationStore.CurrentViewModel = value;
            }
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
