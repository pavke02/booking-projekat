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
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.ViewModel.Guide;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class CreateTourViewModel: ViewModelBase
    {
        #region props
        public ObservableCollection<Tour> TodaysTours { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public ObservableCollection<TourPoint> AllCheckpoints { get; set; }
        public ObservableCollection<String> Checkpoints { get; set; }
        public TourReview _tourReview { get; set; }
        private Tour _tour { get; set; }
        private TourReviewService _tourReviewService { get; set; }
        public List<string> Cities { get; set; }
        private TourRequestService _tourRequestService;



        public Tour Tour { get; set; }
        public Tour Tour1 { get; set; }

        private TextBox _textBox;
        private TourService _tourService;

        private TourPointService _tourPointService;
        private ConfirmTourService _confirmTourService;
        private ConfirmTour _confirmTour;
        private UserService _userService;
      

        private Tour selectedTour;
        public Tour SelectedTour
        {
            get { return selectedTour; }
            set
            {
                if (value != selectedTour)
                {
                    selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRadioButton1Checked;
        public bool IsRadioButton1Checked
        {
            get { return _isRadioButton1Checked; }
            set
            {
                _isRadioButton1Checked = value;
            }
        }

        private bool _isRadioButton2Checked;
        public bool IsRadioButton2Checked
        {
            get { return _isRadioButton2Checked; }
            set
            {
                _isRadioButton2Checked = value;
            }
        }

        private string tourPointName;
        public string TourPointName
        {
            get { return tourPointName; }
            set
            {
                if (value != tourPointName)
                {
                    tourPointName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool checkedCheckBox;

        public bool CheckedCheckBox
        {
            get { return checkedCheckBox; }
            set
            {
                if (checkedCheckBox != value)
                {
                    checkedCheckBox = value;
                    OnPropertyChanged(nameof(CheckedCheckBox));
                }
            }
        }

        private TimeOnly _tourTime;

        public TimeOnly TourTime
        {
            get { return _tourTime; }
            set
            {
                if (value != _tourTime)
                {
                    _tourTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string tourName;

        public string TourName
        {
            get { return tourName; }
            set
            {
                if (value != tourName)
                {
                    tourName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string language;

        public string Languages
        {
            get { return language; }
            set
            {
                if (value != language)
                {
                    language = value;
                    OnPropertyChanged();
                }
            }
        }


        private int maxGuest;

        public int MaxGuest
        {
            get => maxGuest;
            set
            {
                if (value != maxGuest)
                {
                    maxGuest = value;
                    OnPropertyChanged();
                }
            }
        }

        private int time;

        public int Times
        {
            get => time;
            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }


        private string descriptions;

        public string Descriptions
        {
            get => descriptions;
            set
            {
                if (value != descriptions)
                {
                    descriptions = value;
                    OnPropertyChanged();
                }
            }
        }



        private string city;

        public string City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;

        public string Country
        {
            get => country;
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged();
                }

            }
        }

        private string _points;

        public string tourPoints
        {
            get => _points;
            set
            {
                if (value != _points)
                {
                    _points = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime startTour;

        public DateTime StartTour
        {
            get => startTour;
            set
            {
                if (value != startTour)
                    startTour = value;
            }
        }

        private string imageURLs;

        public string ImageURLs
        {
            get => imageURLs;
            set
            {
                if (value != imageURLs)
                    imageURLs = value;
            }
        }

        private string tourPointArray;

        public string TourPointArray
        {
            get => tourPointArray;
            set
            {
                if (value != tourPointArray)
                {
                    tourPointArray = value;
                }
            }
        }
        #endregion

        private readonly ModalNavigationStore _modalNavigationStore;
        private CreateTourViewModel _viewModel;
        private MainWindowViewModel _mainViewModel;
        public ICommand NavigateToStatistics { get; }
        public ICommand NavigateToTourReview { get; }
        public ICommand NavigateToStartingTour { get; }
        public ICommand BackCommand { get; }

        public CreateTourViewModel(TourService tourService, ConfirmTourService confirmTourService,
            TourPointService tourPointService, TextBox textBox, UserService userService,
            Tour tour, TourReviewService tourReviewService, NavigationStore navigationStore, ModalNavigationStore modalNavigationStore,MainWindowViewModel mainViewModel,TourRequestService tourRequestService)
        {
                        
            _textBox = textBox;
            _tour = tour;
            _tourReviewService = tourReviewService;
            _tourService = tourService;
            _tourPointService = tourPointService;
            _userService = userService;
            _confirmTourService = confirmTourService;
            _mainViewModel = mainViewModel;
            _modalNavigationStore = modalNavigationStore;
            _tourRequestService = tourRequestService;

            

            BackCommand = new NavigateCommand(CreateCloseModalNavigationService(modalNavigationStore));


            Cities = new List<string> { "Serbia,Novi Sad", "Serbia,Ruma", "Serbia,Belgrade", "Serbia, Nis", "England,London", "England,London EAST" };

            TodaysTours = new ObservableCollection<Tour>(_tourService.GetTodaysTours());
            AllTours = new ObservableCollection<Tour>(_tourService.GetAll());
            AllCheckpoints = new ObservableCollection<TourPoint>(_tourPointService.GetAll());
            Checkpoints = new ObservableCollection<string>();

            NavigateToStartingTour = new NavigateCommand(CreateStartingTourModalService(navigationStore, modalNavigationStore), this,
                () => SelectedTour != null);
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
        private INavigationService CreateStartingTourModalService(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore)
        {
            return new NavigationService<StartTourViewModel>
                (navigationStore, () => new StartTourViewModel(SelectedTour, _confirmTourService, this, navigationStore, modalNavigationStore));
        }

 
        private void UpdateTour(List<Tour> tours, List<Tour> todaysTours)
        {
            AllTours.Clear();
            foreach (var tour in tours)
                AllTours.Add(tour);

            TodaysTours.Clear();
            foreach (var tour in todaysTours)
                TodaysTours.Add(tour);
        }

        public void Update()
        {
            UpdateTour(_tourService.GetAll(), _tourService.GetTodaysTours());
        }

        [RelayCommand]
        private void AddImage()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                if (ImageURLs == "")
                {
                    ImageURLs = filePath;
                }
                else
                {
                    ImageURLs = ImageURLs + "\n" + filePath;
                }
            }
        }

       

        

        [RelayCommand]
        private void AddTour()
        {
            int count = _tourService.CountCheckPoints(TourPointArray);

            if (count < 2)
                MessageBox.Show("Unesite najmanje dva checkpointa!");
            else
            {
                List<string> imageURLs = new List<string>();
                string[] values = ImageURLs.Split("\n");
                foreach (string value in values)
                    imageURLs.Add(value);


                List<int> TourPointIds = new List<int>();
                List<TourPoint> TourPoints = new List<TourPoint>();

                List<string> tourPoinNames = TourPointArray.Split(",").ToList();
                bool isFirst = true;
                foreach (string tourPoinName in tourPoinNames)
                {
                    TourPoint tourPoint = new TourPoint(tourPoinName, isFirst);
                    isFirst = false;
                    _tourPointService.Save(tourPoint);
                    TourPoints.Add(tourPoint);
                    TourPointIds.Add(tourPoint.GetId());
                }



               

                DateTime time = new DateTime(StartTour.Year, StartTour.Month, StartTour.Day, TourTime.Hour, TourTime.Minute, TourTime.Second);






                if (IsRadioButton1Checked)
                {
                    string[] v = City.Split(",");
                    Location location = new Location(v[0], v[1]);
                    Tour tour = new Tour(TourName, location, Descriptions, _tourRequestService.MostCommonLanguage(), MaxGuest, time, Times, imageURLs, TourPointIds, TourPoints, TourTime);
                    _tourService.Save(tour);
                }
                else if (IsRadioButton2Checked)
                {
                    Location location = new Location(_tourRequestService.MostCommonLocation().ElementAt(0).Key, _tourRequestService.MostCommonLocation().ElementAt(0).Value);

                    Tour tour = new Tour(TourName, location, Descriptions, _tourRequestService.MostCommonLanguage(), MaxGuest, time, Times, imageURLs, TourPointIds, TourPoints, TourTime);
                    _tourService.Save(tour);

                }
                else
                {

                    string[] v = City.Split(",");
                    Location location = new Location(v[0], v[1]);


                    time = new DateTime(StartTour.Year, StartTour.Month, StartTour.Day, TourTime.Hour, TourTime.Minute, TourTime.Second);
                    Tour tour = new Tour(TourName, location, Descriptions, Languages, MaxGuest, time, Times, imageURLs, TourPointIds, TourPoints, TourTime);
                    _tourService.Save(tour);
                }

               
            }
        }

       
        public void AutoGenerateLanguageField()
        {
            if(IsRadioButton1Checked)
            {
                string Languages = _tourRequestService.MostCommonLanguage();
            }
        }

    }
}
