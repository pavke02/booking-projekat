using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service;
using System.Windows.Input;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class TourRequestByNumberOfGuestsViewModel:ViewModelBase
    {
        public TourRequestService _tourRequestService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<TourRequest> TourRequestByGusts { get; set; }
        private TourService _tourService;

        private Location _city;
        public Location City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _guests;
        public int DateStart
        {
            get { return _guests; }
            set
            {
                if (value != _guests)
                {
                    _guests = value;
                    OnPropertyChanged();
                }
            }
        }



      

        public ICommand BackCommand { get; }
        public ICommand AcceptCommand { get; }
        private MainWindowViewModel _mainViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private TourRequest selectedTour;
        public TourRequest SelectedTour
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

        public TourRequestByNumberOfGuestsViewModel(TourRequest selectedTour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore, NavigationStore navigationStore, TourRequestService tourRequestService,int guests, TourService tourService)
        {
            SelectedTour = selectedTour;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;
            _guests = guests;
            _tourService = tourService;



            TourRequestByGusts = new ObservableCollection<TourRequest>(_tourRequestService.GetToursByNumberOfGuests(_guests));
            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(navigationStore));

        }

        private INavigationService CreateCloseModalNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<MainWindowViewModel>
            (navigationStore, () => _mainViewModel);

        }

        //[RelayCommand]
        //public void AddRequestedTour()
        //{

        //    if (_tourService.IsFreeGuide(SelectedTour.TimeOfStart))
        //    {
        //        SelectedTour.Requests = Enums.Requests.Accepted;
        //        _tourRequestService.Update(SelectedTour);
        //    }
        //}
    }
}
