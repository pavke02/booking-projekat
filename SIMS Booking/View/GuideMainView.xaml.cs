using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;


namespace SIMS_Booking.View
{

    public partial class GuideMainView : Window, INotifyPropertyChanged, IObserver
    {
        public ObservableCollection<Tour> TodaysTours { get; set; }
        public ObservableCollection<Tour> AllTours { get; set; }
        public ObservableCollection <String> Checkpoints{ get; set; }

        public Tour Tour { get; }

        private TourRepository _tourRepository;
        public Tour SelectedTour { get; set; }

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



        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuideMainView(TourRepository  tourRepository)
        {
            InitializeComponent();
            DataContext = this;
            Tour = new Tour();
           
            _tourRepository = tourRepository;
            _tourRepository.Subscribe(this);
            TodaysTours = new ObservableCollection<Tour>(_tourRepository.GetTodaysTours());
            AllTours = new ObservableCollection<Tour>(_tourRepository.GetAll());
          //  Checkpoints = new ObservableCollection<string>(SelectedTour.tourPoints);




        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTour != null)
            {
                StartTour startTour = new StartTour(SelectedTour);
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

            Tour tour = new Tour();

            tour.Name = Name.Text;
            tour.Description = Description.Text;
            int number;
            if (int.TryParse(MaxGuests.Text, out number))
            {
                tour.MaxGuests = number;
                
            }
            else
            {
                MessageBox.Show("Please enter a valid number.");
            }

            Location location = new Location("Srbija", "Novi Sad");
            tour.Location = location;


            int vreme;
            if (int.TryParse(Time.Text, out vreme))
            {
                tour.Time = vreme;
            }
            else
            {
                MessageBox.Show("Please enter a valid number for time.");
            }


            
            tour.StartTour = myTimePicker.SelectedDate.Value;

            _tourRepository.Save(tour);
            AllTours.Add(tour);

        }
    }
}
