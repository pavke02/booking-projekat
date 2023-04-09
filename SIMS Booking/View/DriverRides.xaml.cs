using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace SIMS_Booking.View
{
    public partial class DriverRides : Window
    {
        private DispatcherTimer timer;
        private DispatcherTimer timer2;

        private int startingPrice = 190;
        private int timerTickCounter = 0;

        public Rides selectedRide;

        private RidesRepository _ridesRepository;
        private FinishedRidesRepository _finishedRidesRepository;
        public User User { get; set; }

        public static ObservableCollection<Rides> Rides { get; set; }

        public List<Rides> ActiveRides { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DriverRides(User user, RidesRepository ridesRepository, FinishedRidesRepository finishedRidesRepository)
        {
            InitializeComponent();
            DataContext = this;

            _ridesRepository = ridesRepository;
            _finishedRidesRepository = finishedRidesRepository;
            User = user;

            Rides = new ObservableCollection<Rides>(_ridesRepository.GetAll());
            ActiveRides = new List<Rides>();

            foreach (Rides ride in Rides)
            {
                if(ride.DriverID == User.getID() && ride.DateTime.Date == DateTime.Now.Date && ride.DateTime > DateTime.Now)
                {
                    ActiveRides.Add(ride);
                }
            }

            

        }

        private int remainingTime;
        private TimeSpan timeDif;

        private void arrivedButton_Click(object sender, RoutedEventArgs e)
        {

            selectedRide = ridesGrid.SelectedItem as Rides;

            timeDif = selectedRide.DateTime - DateTime.Now;

            if (timeDif.TotalSeconds <= 300)
            {
                remainingTime = (int)(20 * 60 + timeDif.TotalSeconds);

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;

                timer.Start();
                arrivedButton.IsEnabled = false;
                lateButton.IsEnabled = false;
                startButton.IsEnabled = true;
                cancelButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("You arrived too soon!");
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
            RemainingTimeLabel.Content = timeSpan.ToString(@"mm\:ss");

            if (remainingTime == 0)
            {
                timer.Stop();
                MessageBox.Show("Guest is late!");
            }
        }

        private void lateButton_Click(object sender, RoutedEventArgs e)
        {
            selectedRide = ridesGrid.SelectedItem as Rides;

            timeDif = selectedRide.DateTime - DateTime.Now;

            if (timeDif.TotalSeconds <= 300)
            {

                DriverLate driveLate = new DriverLate();
                driveLate.ShowDialog();


                remainingTime = (int)(20 * 60 + 60 * driveLate.LateInMinutes + timeDif.TotalSeconds);

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;

                timer.Start();
                arrivedButton.IsEnabled = false;
                lateButton.IsEnabled = false;
                startButton.IsEnabled = true;
                cancelButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("You arrived too soon!");
            }
            

            
        }

        private void ridesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            arrivedButton.IsEnabled = true;
            lateButton.IsEnabled = true;
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {

            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += Timer2_Tick;
            timer2.Start();

            StartingPriceLabel.Content = startingPrice.ToString() + " RSD";

            StopwatchLabel.Content = "00:00:00";

            stopButton.Visibility = Visibility.Visible;
            timer.Stop();
            RemainingTimeLabel.Content = "";
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            timerTickCounter++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerTickCounter);
            StopwatchLabel.Content = timeSpan.ToString(@"hh\:mm\:ss");
            startingPrice += 2;
            StartingPriceLabel.Content = startingPrice.ToString() + " RSD";
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {   
            timer.Stop();
            MessageBox.Show("Guest is late!");
            startButton.IsEnabled = false;
            cancelButton.IsEnabled = false;
            RemainingTimeLabel.Content = "";
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            timer2.Stop();
            MessageBox.Show("Ride successfully finished!");

            FinishedRide selectedFinishedRide = new FinishedRide();
            selectedFinishedRide.Ride = selectedRide;
            selectedFinishedRide.Price = (string)StartingPriceLabel.Content;
            selectedFinishedRide.Time = (string)StopwatchLabel.Content;

            StartingPriceLabel.Content = "";
            StopwatchLabel.Content = "";

            ActiveRides.Remove(selectedRide);
            ridesGrid.Items.Refresh();

            _ridesRepository.Delete(selectedRide);
            _finishedRidesRepository.Save(selectedFinishedRide);

        }
    }
}
