using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public partial class TourComplexRequestViewModel: ViewModelBase
    {

        public TourRequestService _tourRequestService;
        public string AllPictures;
        public ObservableCollection<TourRequestComplex> TourRequestsComplex { get; set; }
        public ObservableCollection<DateTime> FreeDates { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public ObservableCollection<TourRequest> TourRequestBydate { get; set; }
        public ICommand ComboBoxFreeDates { get; }
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

        private TourRequestComplex _tourRequestComplex;

        private TourService _tourService;
        public ICommand BackCommand { get; }
        public ICommand AcceptCommand { get; }
        public ICommand FilterByDateCommand { get; }
        public ICommand NavigateCommand { get; }
        public ICommand FilterByLocationCommand { get; }
        public ICommand FilterByLanguageCommand { get; }
        public ICommand FilterByNumberOfGuestsCommand { get; }
        public ICommand ComplexTour { get; }

        private CreateTourViewModel _createTourViewModel;
        private MainWindowViewModel _mainViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private NavigationStore _navigationStore;
        private TourRequestComplexService _tourRequestComplexService;
        private TourRequestComplex _selectedComplexTour;
        public TourRequestComplex SelectedComplexTour
        {
            get { return _selectedComplexTour; }
            set
            {
                if (value != _selectedComplexTour)
                {
                    _selectedComplexTour = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MozeLiKomso));
                }
            }
        }
        public bool MozeLiKomso => SelectedComplexTour != null;



        

        private DateTime _selectedComplexTourFromComboBox;
        public DateTime SelectedComplexTourFromComboBox
        {
            get { return _selectedComplexTourFromComboBox; }
            set
            {
                if (value != _selectedComplexTourFromComboBox)
                {
                    _selectedComplexTourFromComboBox = value;
                    OnPropertyChanged();
                    
                }
            }
        }


        public TourComplexRequestViewModel(TourRequestComplex selectedComplexTour, MainWindowViewModel mainViewModel, ModalNavigationStore modalNavigationStore, NavigationStore navigationStore, TourRequestComplexService tourRequestComplexService, TourService tourService, CreateTourViewModel createTourViewModel, TourRequestComplex tourRequestComplex)
        {
            SelectedComplexTour = selectedComplexTour;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestComplexService = tourRequestComplexService;
            _tourService = tourService;
            _navigationStore = navigationStore;
            _createTourViewModel = createTourViewModel;
            _tourRequestComplex = tourRequestComplex; ;


            TourRequestsComplex = new ObservableCollection<TourRequestComplex>(_tourRequestComplexService.GetValidTourRequestForGuide());
            
            

        }
        [RelayCommand]
        public void ConfirmComplexTour(TourRequestComplex selectedComplexTour)
        {
            FreeDates = new ObservableCollection<DateTime>(_tourRequestComplexService.ListAllFreeDates(_tourService, _selectedComplexTour));
            OnPropertyChanged(nameof(FreeDates));
           
        }


        [RelayCommand]
        public void AcceptComplexTour(TourRequestComplex selectedComplexTour)
        {
            if (_selectedComplexTourFromComboBox != null)
            {
                _selectedComplexTour.Requests = Enums.Requests.Accepted;
                _selectedComplexTour.TimeOfStart = _selectedComplexTourFromComboBox;
                // selectedComplexTour.Update();

            }
        }


    }

}

