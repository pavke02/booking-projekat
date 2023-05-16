using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class TourRequestByDateViewModel:ViewModelBase
    {
        public TourRequestService _tourRequestService;
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<TourRequest> TourRequestBydate { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        private CreateTourViewModel _createTourViewModel;

        private DateTime _exactDate;
        public DateTime ExactDate
        {
            get { return _exactDate; }
            set
            {
                if (value != _exactDate)
                {
                    _exactDate = value;
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

       
        public ICommand BackCommand { get; }
        public ICommand AcceptCommand { get; }
        private MainWindowViewModel _mainViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private TourService _tourService;
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
                    //OnPropertyChanged(nameof(MozeLiKomso));
                }
            }
        }
        public bool MozeLiKomso => SelectedTour != null;

        public TourRequestByDateViewModel(TourRequest selectedTour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore, NavigationStore navigationStore, TourRequestService tourRequestService, DateTime start, DateTime end, TourService tourService, CreateTourViewModel createTourViewModel)
        {
            SelectedTour = selectedTour;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;
            _dateStarts = start;
            _dateEnds = end;
            _tourService = tourService;
            _createTourViewModel = createTourViewModel;


            TourRequestBydate = new ObservableCollection<TourRequest>(_tourRequestService.GetRequestToursByDate(_dateStarts, _dateEnds));
            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(navigationStore));
           
        }

        private INavigationService CreateCloseModalNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<MainWindowViewModel>
            (navigationStore, () => _mainViewModel);

        }

        [RelayCommand]
        public void AddRequestedTour()
        {

            if (_tourService.IsFreeGuideInRangeOfDates(SelectedTour.TimeOfStart,SelectedTour.TimeOfEnd, ExactDate))
            {
                if (_tourService.IsFreeGuideInOtherTours(ExactDate))
                {
                SelectedTour.DefaultDate = ExactDate;
                SelectedTour.Requests = Enums.Requests.Accepted;
                _tourRequestService.Update(SelectedTour);
                }
            }
            else
            {
                MessageBox.Show("Vodic je tada zauzet");
            }
        }

        
    }
}
