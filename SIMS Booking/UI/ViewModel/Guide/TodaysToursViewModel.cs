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
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class TodaysToursViewModel : ViewModelBase
    {
        public TourService _tourService;
        public string AllPictures;
        public ObservableCollection<Tour> NowTours { get; set; }
        private Tour _tour;
        private Tour selectedTour;
        public ICommand BackCommand { get; }
        private MainWindowViewModel _mainViewModel;
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
        
        public TodaysToursViewModel(TourService tourService, Tour tour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore)
        {
            _tourService = tourService;
            _tour = tour;
            _mainViewModel = mainViewModel;

            NowTours = new ObservableCollection<Tour>(_tourService.GetTodaysTours());
            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(modalNavigationStore));


        }
        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

    }
}
