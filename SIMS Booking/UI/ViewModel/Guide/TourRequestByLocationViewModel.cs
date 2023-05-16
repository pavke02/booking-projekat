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
    public partial class TourRequestByLocationViewModel:ViewModelBase
    {

        public TourRequestService _tourRequestService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<TourRequest> TourRequestByLocation { get; set; }
        private TourService _tourService;

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }



        private string _city;
        public string City
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

        private string _country;
        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
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

        public TourRequestByLocationViewModel(TourRequest selectedTour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore, NavigationStore navigationStore, TourRequestService tourRequestService, string country, string city, TourService tourService)
        {
            SelectedTour = selectedTour;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            SelectedTour = selectedTour;
            _tourRequestService = tourRequestService;
            _country = country;
            _city = city;
            _tourService = tourService;



            TourRequestByLocation = new ObservableCollection<TourRequest>(_tourRequestService.GetRequestToursByLocation(country,city));
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
