using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class CompletedToursViewModel: ViewModelBase
    {
        public TourService _tourService;
        public List<string> AllPictures;
        public ObservableCollection<Tour> CompletedToursList { get; set; }
        private Tour _tour;
        public ICommand BackCommand { get; }
        private MainWindowViewModel _mainViewModel;
        private Tour selectedTour;
        public Tour SelectedTour
        {
            get { return selectedTour; }
            set
            {
                if (value != selectedTour)
                {
                    selectedTour = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MozeLiKomso));
                }
            }
        }

        public bool MozeLiKomso => SelectedTour != null;
        public CompletedToursViewModel(TourService tourService, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore) 
        {
            _tourService = tourService;
            _mainViewModel = mainViewModel;


            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(modalNavigationStore));

            CompletedToursList = new ObservableCollection<Tour>(_tourService.GetCompletedTours());
        }
        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
