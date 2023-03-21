﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;


namespace SIMS_Booking.View
{

    public partial class GuideMainView : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<Tour> TodaysTours { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public ObservableCollection<TourPoint> AllCheckpoints { get; set; }
        public ObservableCollection <String> Checkpoints{ get; set; }

        public Tour Tour { get; set; }
        public Tour Tour1 { get; set; }
        public TourPoint dataTourPoint { get; set; }

        private TourRepository _tourRepository;
        private ObservableCollection<Tour> _allToursRepository;
        private TourPointRepository _tourPointRepository;
        private ConfirmTourRepository _confirmTourRepository;
 
        public Tour SelectedTour { get; set; }

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


        private string maxGuest;
        public string MaxGuest
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
        private string time;
        public string Times
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
            get =>  descriptions;
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
                {
                    startTour = value;
                }
            }
        }

        private string imageURLs;
        

        public string ImageURLs
        {
            get => imageURLs;
            set
            {
                if (value != imageURLs)
                {
                    imageURLs = value;
                }
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


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuideMainView(TourRepository  tourRepository, ConfirmTourRepository confirmTourRepository, TourPointRepository tourPointRepository )
        {
            InitializeComponent();
            DataContext = this;
            Tour = new Tour();
            Tour1 = new Tour();
           
            _tourRepository = tourRepository;
            _tourRepository.Subscribe(this);

            _tourPointRepository = tourPointRepository;
            _tourPointRepository.Subscribe(this);

            _confirmTourRepository = confirmTourRepository;
            //_tour - Tour;

            TodaysTours = new ObservableCollection<Tour>(_tourRepository.GetTodaysTours());
            AllTours = new ObservableCollection<Tour>(_tourRepository.GetAll());
            AllCheckpoints = new ObservableCollection<TourPoint>(_tourPointRepository.GetAll());
            Checkpoints = new ObservableCollection<string>();


        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null)
            {
                StartTour startTour = new StartTour(SelectedTour, _confirmTourRepository);
                startTour.ShowDialog();
            }

           
        }

        private void UpdateTour(List<Tour> tours)
        {
            AllTours.Clear();
            foreach (var tour in tours)
                AllTours.Add(tour);
        }

        public void Update()
        {
            UpdateTour(_tourRepository.GetAll());
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {


            Location location = new Location("Serbia", "Novi Sad");

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

                tourPoint = _tourPointRepository.Save(tourPoint);
                TourPoints.Add(tourPoint);
                TourPointIds.Add(tourPoint.getID());


            }



            Tour tour = new Tour(TourName, location, Descriptions, Languages, int.Parse(MaxGuest), StartTour, int.Parse(Times), imageURLs, TourPointIds, TourPoints);
             _tourRepository.Save(tour);




        }



       

        private void AddImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            bool? response = openFileDialog.ShowDialog();

            if (response == true)
            {
                string filePath = openFileDialog.FileName;
                if (imageTb.Text == "")
                {
                    imageTb.Text = filePath;
                    ImageURLs = imageTb.Text;
                }
                else
                {
                    imageTb.Text = imageTb.Text + "\n" + filePath;
                    ImageURLs = imageTb.Text;
                }
            }
        }
    }
}
