using SIMS_Booking.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Commands.DriverCommands;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverRidesViewModel : ViewModelBase
    {
        public DispatcherTimer timer;
        public DispatcherTimer timer2;

        private int startingPrice = 190;
        private int timerTickCounter = 0;

        private RidesService _ridesService;
        private FinishedRidesService _finishedRidesService;
        private VehicleService _vehicleService;
        public User User { get; set; }

        public static ObservableCollection<Rides> Rides { get; set; }
        public static ObservableCollection<Rides> ActiveRides { get; set; }
        //public List<Rides> ActiveRides { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand NavigateToDriverLateCommand { get; }
        public ICommand ArrivedOnLocationCommand { get; }
        public ICommand StartRideCommand { get; }
        public ICommand StopRideCommand { get; }
        public ICommand ArrivedOnLocationLateCommand { get; }


        //public ObservableCollection<Rides> ActiveRides { get; set; }

        private Rides _selectedRide;
        public Rides SelectedRide
        {
            get => _selectedRide;
            set
            {
                if (value != _selectedRide)
                {
                    _selectedRide = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _remainingTime;
        public string RemainingTime
        {
            get => _remainingTime;
            set
            {
                if (value != _remainingTime)
                {
                    _remainingTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _price;
        public string Price
        {
            get => _price;
            set
            {
                if (value != _price)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _rSDString;
        public string RSDString
        {
            get => _rSDString;
            set
            {
                if (value != _rSDString)
                {
                    _rSDString = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _taximeter;
        public string Taximeter
        {
            get => _taximeter;
            set
            {
                if (value != _taximeter)
                {
                    _taximeter = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _lateInMinutes;
        public string LateInMinutes
        {
            get => _lateInMinutes;
            set
            {
                if (value != _lateInMinutes)
                {
                    _lateInMinutes = value;
                    OnPropertyChanged();
                }
            }
        }

        public DriverRidesViewModel(User user, RidesService ridesService, FinishedRidesService finishedRidesService, VehicleService vehicleService, ModalNavigationStore modalNavigationStore)
        {

            _ridesService = ridesService;
            _finishedRidesService = finishedRidesService;
            _vehicleService = vehicleService;

            User = user;

            Vehicle vehicle = _vehicleService.GetVehicleByUserID(user.getID());

            Rides = new ObservableCollection<Rides>(_ridesService.GetAll());
            ActiveRides = new ObservableCollection<Rides>(_ridesService.GetActiveRides(User, vehicle));
            //ActiveRides = new List<Rides>();

            //foreach (Rides ride in Rides)
            //{
            //    bool onLocation = false;
            //    foreach (Location location in vehicle.Locations)
            //    {
            //        if (location.City == ride.Location.City && location.Country == ride.Location.Country)
            //        {
            //            onLocation = true;
            //        }
            //    }
            //    if ((ride.DriverID == User.getID() && ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now) || (ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now && ride.Fast == true && onLocation == true))
            //    {
            //        ActiveRides.Add(ride);
            //    }
            //}
            NavigateBackCommand = new NavigateBackCommand(CreateCloseRidesNavigationService(modalNavigationStore));

            ArrivedOnLocationCommand = new ArrivedCommand(this);
            StartRideCommand = new StartCommand(this);
            StopRideCommand = new StopCommand(this, _finishedRidesService, _ridesService);
            ArrivedOnLocationLateCommand = new LateArrivedCommand(this);
        }

        //private int remainingTime;
        //private TimeSpan timeDif;

        //private void arrivedButton_Click(object sender, RoutedEventArgs e)
        //{

        //    selectedRide = ridesGrid.SelectedItem as Rides;

        //    timeDif = selectedRide.DateTime - DateTime.Now;

        //    if (timeDif.TotalSeconds <= 300)
        //    {
        //        remainingTime = (int)(20 * 60 + timeDif.TotalSeconds);

        //        timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(1);
        //        timer.Tick += Timer_Tick;

        //        timer.Start();
        //        arrivedButton.IsEnabled = false;
        //        lateButton.IsEnabled = false;
        //        startButton.IsEnabled = true;
        //        cancelButton.IsEnabled = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("You arrived too soon!");
        //    }
        //}


        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    remainingTime--;

        //    TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
        //    RemainingTimeLabel.Content = timeSpan.ToString(@"mm\:ss");

        //    if (remainingTime == 0)
        //    {
        //        timer.Stop();
        //        MessageBox.Show("Guest is late!");
        //    }
        //}

        //private void lateButton_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedRide = ridesGrid.SelectedItem as Rides;

        //    timeDif = selectedRide.DateTime - DateTime.Now;

        //    if (timeDif.TotalSeconds <= 300)
        //    {
        //        DriverLate driveLate = new DriverLate();
        //        driveLate.ShowDialog();

        //        remainingTime = (int)(20 * 60 + 60 * driveLate.LateInMinutes + timeDif.TotalSeconds);

        //        timer = new DispatcherTimer();
        //        timer.Interval = TimeSpan.FromSeconds(1);
        //        timer.Tick += Timer_Tick;

        //        timer.Start();
        //        arrivedButton.IsEnabled = false;
        //        lateButton.IsEnabled = false;
        //        startButton.IsEnabled = true;
        //        cancelButton.IsEnabled = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("You arrived too soon!");
        //    }
        //}

        //private void ridesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    arrivedButton.IsEnabled = true;
        //    lateButton.IsEnabled = true;
        //}

        //private void startButton_Click(object sender, RoutedEventArgs e)
        //{
        //    timer2 = new DispatcherTimer();
        //    timer2.Interval = TimeSpan.FromSeconds(1);
        //    timer2.Tick += Timer2_Tick;
        //    timer2.Start();

        //    StartingPriceLabel.Content = startingPrice.ToString() + " RSD";
        //    StopwatchLabel.Content = "00:00:00";

        //    stopButton.Visibility = Visibility.Visible;
        //    timer.Stop();
        //    RemainingTimeLabel.Content = "";
        //}

        //private void Timer2_Tick(object sender, EventArgs e)
        //{
        //    timerTickCounter++;
        //    TimeSpan timeSpan = TimeSpan.FromSeconds(timerTickCounter);
        //    StopwatchLabel.Content = timeSpan.ToString(@"hh\:mm\:ss");
        //    startingPrice += 2;
        //    StartingPriceLabel.Content = startingPrice.ToString() + " RSD";
        //}

        //private void cancelButton_Click(object sender, RoutedEventArgs e)
        //{
        //    timer.Stop();
        //    MessageBox.Show("Guest is late!");
        //    startButton.IsEnabled = false;
        //    cancelButton.IsEnabled = false;
        //    RemainingTimeLabel.Content = "";
        //}

        //private void stopButton_Click(object sender, RoutedEventArgs e)
        //{
        //    timer2.Stop();
        //    MessageBox.Show("Ride successfully finished!");

        //    FinishedRide selectedFinishedRide = new FinishedRide();
        //    selectedFinishedRide.Ride = selectedRide;
        //    selectedFinishedRide.Price = (string)StartingPriceLabel.Content;
        //    selectedFinishedRide.Time = (string)StopwatchLabel.Content;

        //    StartingPriceLabel.Content = "";
        //    StopwatchLabel.Content = "";

        //    ActiveRides.Remove(selectedRide);
        //    ridesGrid.Items.Refresh();

        //    _ridesService.Delete(selectedRide);
        //    _finishedRidesService.Save(selectedFinishedRide);

        //    startButton.IsEnabled = false;
        //    cancelButton.IsEnabled = false;
        //    stopButton.Visibility = Visibility.Hidden;
        //}

        private INavigationService CreateCloseRidesNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
