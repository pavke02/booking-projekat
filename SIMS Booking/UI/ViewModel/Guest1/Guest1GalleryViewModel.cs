using SIMS_Booking.Model;
using SIMS_Booking.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Utility.Stores;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Guest1
{
    internal class Guest1GalleryViewModel : ViewModelBase
    {
        public Accommodation SelectedAccommodation { get; set; }
        public List<string> ImageUrlsLeft { get; set; }
        public List<string> ImageUrlsRight { get; set; }

        public ICommand NavigateBackCommand { get; }

        public Guest1GalleryViewModel(ModalNavigationStore modalNavigationStore,Accommodation selectedAccommodation)
        {
            SelectedAccommodation = selectedAccommodation;
            LoadImages();
            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        private void LoadImages()
        {
            ImageUrlsLeft = new List<string>();
            ImageUrlsRight = new List<string>();

            for (int i = 0; i < SelectedAccommodation.ImageURLs.Count / 2; i++)
            {
                ImageUrlsLeft.Add(SelectedAccommodation.ImageURLs.ElementAt(i));
            }
            for (int i = SelectedAccommodation.ImageURLs.Count / 2; i < SelectedAccommodation.ImageURLs.Count; i++)
            {
                ImageUrlsRight.Add(SelectedAccommodation.ImageURLs.ElementAt(i));
            }
        }
    }
}
