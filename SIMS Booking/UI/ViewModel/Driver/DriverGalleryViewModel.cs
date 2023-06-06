using SIMS_Booking.Model;
using SIMS_Booking.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverGalleryViewModel : ViewModelBase
    {
        public Vehicle Vehicle { get; set; }
        public List<string> ImageUrlsLeft { get; set; }
        public List<string> ImageUrlsRight { get; set; }
        public ICommand NavigateBackCommand { get; }

        public DriverGalleryViewModel(Vehicle vehicle, ModalNavigationStore modalNavigationStore)
        {
            Vehicle = vehicle;
            LoadImages();
            NavigateBackCommand = new NavigateBackCommand(CreateCloseGalleryNavigationService(modalNavigationStore));
        }

        private void LoadImages()
        {
            ImageUrlsLeft = new List<string>();
            ImageUrlsRight = new List<string>();

            for (int i = 0; i < Vehicle.ImagesURL.Count / 2; i++)
            {
                ImageUrlsLeft.Add(Vehicle.ImagesURL.ElementAt(i));
            }
            for (int i = Vehicle.ImagesURL.Count / 2; i < Vehicle.ImagesURL.Count; i++)
            {
                ImageUrlsRight.Add(Vehicle.ImagesURL.ElementAt(i));
            }
        }

        private INavigationService CreateCloseGalleryNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
