using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class FutureToursViewModel: ViewModelBase
    {
        public TourService _tourService;
        public List<string> AllPictures;
        public ObservableCollection<Tour> FutureToursList { get; set; }
        private Tour _tour;
        public ICommand BackCommand { get; }
        private MainWindowViewModel _mainViewModel;
        private ModalNavigationStore _modalNavigationStore;

        public FutureToursViewModel(TourService tourService, MainWindowViewModel mainViewModel,ModalNavigationStore modalNavigationStore)
        {
            _tourService = tourService;
            _modalNavigationStore = modalNavigationStore;


            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(modalNavigationStore));
            FutureToursList = new ObservableCollection<Tour>(_tourService.GetFutureTours());
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

    }
}
