using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;
using System.Collections;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class TourRequestViewModel : ViewModelBase
    {
        public TourRequestService _tourRequestService;
        public string AllPictures;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public ObservableCollection<TourRequest> TourRequestBydate { get; set; }

        private string _stats;
        public string Stats
        {
            get { return _stats; }
            set
            {
                if (value != _stats)
                {
                    _stats = value;
                    OnPropertyChanged();
                }
            }
        }



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



        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                if (value != _year)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }



        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                if (value != _month)
                {
                    _month = value;
                    OnPropertyChanged();
                }
            }
        }



        private int _guests;
        public int Guests
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


        private DateTime _dateStarts;
        public DateTime DateStart
        {
            get { return _dateStarts; }
            set
            {
                if (value != _dateStarts)
                {
                    _dateStarts = value;
                    OnPropertyChanged();
                }
            }
        }



        private DateTime _dateEnds;
        public DateTime DateEnds
        {
            get { return _dateEnds; }
            set
            {
                if (value != _dateEnds)
                {
                    _dateEnds = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourRequest _tourRequest;

        private TourService _tourService;
        public ICommand BackCommand { get; }
        public ICommand AcceptCommand { get; }
        public ICommand FilterByDateCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand FilterByLocationCommand { get; }
        public ICommand FilterByLanguageCommand { get; }
        public ICommand FilterByNumberOfGuestsCommand { get; }
        private CreateTourViewModel _createTourViewModel;
        private MainWindowViewModel _mainViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private NavigationStore _navigationStore;
        private TourRequest _selectedTour;
        public TourRequest SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MozeLiKomso));
                }
            }
        }
        public bool MozeLiKomso => SelectedTour != null;

        public TourRequestViewModel(TourRequest selectedTour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore, NavigationStore navigationStore, TourRequestService tourRequestService, TourService tourService, CreateTourViewModel createTourViewModel, TourRequest tourRequest)
        {
            SelectedTour = selectedTour;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;
            _tourService = tourService;
            _navigationStore = navigationStore;
            _createTourViewModel = createTourViewModel;
            _tourRequest = tourRequest; ;



            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetRequestTours());
            TourRequestBydate = new ObservableCollection<TourRequest>(_tourRequestService.GetRequestToursByDate(_dateStarts, _dateEnds));
            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(navigationStore));
            FilterByDateCommand = new NavigateCommand(CreateFilterBydatelNavigationService(navigationStore));
            FilterByNumberOfGuestsCommand = new NavigateCommand(CreateFilterByNumberOfGuestslNavigationService(navigationStore));
            FilterByLanguageCommand = new NavigateCommand(CreateFilterByLanguagelNavigationService(navigationStore));
            FilterByLocationCommand = new NavigateCommand(CreateFilterByLocationlNavigationService(navigationStore));



        }

        private INavigationService CreateCloseModalNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<MainWindowViewModel>
            (navigationStore, () => _mainViewModel);

        }

        private INavigationService CreateFilterBydatelNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TourRequestByDateViewModel>
            (navigationStore, () => new TourRequestByDateViewModel(SelectedTour,_mainViewModel, _modalNavigationStore, navigationStore, _tourRequestService,_dateStarts,_dateEnds,_tourService,_createTourViewModel));

        }
        private INavigationService CreateFilterByNumberOfGuestslNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TourRequestByNumberOfGuestsViewModel>
            (navigationStore, () => new TourRequestByNumberOfGuestsViewModel(SelectedTour, _mainViewModel, _modalNavigationStore, navigationStore, _tourRequestService,_guests,_tourService));

        }

        private INavigationService CreateFilterByLanguagelNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TourRequestByLanguageViewModel>
            (navigationStore, () => new TourRequestByLanguageViewModel(SelectedTour, _mainViewModel, _modalNavigationStore, navigationStore, _tourRequestService,_language, _tourService));

        }

        private INavigationService CreateFilterByLocationlNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<TourRequestByLocationViewModel>
            (navigationStore, () => new TourRequestByLocationViewModel(SelectedTour, _mainViewModel, _modalNavigationStore, navigationStore, _tourRequestService,_country,_city, _tourService));

        }

        [RelayCommand]
        public void Statistics()
        {            
            if (Year != 0 )
                 Stats = string.Join(Environment.NewLine, _tourRequestService.GetRequestToursStatsByYear(Year,_language)) + "\n";
            else
                 Stats = string.Join(Environment.NewLine, _tourRequestService.GetAllTimeRequestToursStats(_language)) + "\n";
            
           
        }

       
        
        

    }
}
