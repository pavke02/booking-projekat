using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverTutorialViewModel : ViewModelBase
    {
        public ICommand NavigateBackCommand { get; }

        private Uri _videoSource;
        public Uri VideoSource
        {
            get { return _videoSource; }
            set
            {
                _videoSource = value;
                OnPropertyChanged(nameof(VideoSource));
            }
        }

        public DriverTutorialViewModel(ModalNavigationStore modalNavigationStore)
        {
            NavigateBackCommand = new NavigateBackCommand(CreateCloseAddVehicleNavigationService(modalNavigationStore));

            VideoSource = new Uri("../../../Resources/Videos/driverTutorial.mp4", UriKind.RelativeOrAbsolute);
        }

        private INavigationService CreateCloseAddVehicleNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
