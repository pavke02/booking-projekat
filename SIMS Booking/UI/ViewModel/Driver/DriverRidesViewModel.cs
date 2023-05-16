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
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverRidesViewModel : ViewModelBase, IObserver
    {
        public DispatcherTimer timer;
        public DispatcherTimer timer2;

        private RidesService _ridesService;
        private FinishedRidesService _finishedRidesService;
        private VehicleService _vehicleService;
        public User User { get; set; }

        public static ObservableCollection<Rides> Rides { get; set; }
        public static ObservableCollection<Rides> ActiveRides { get; set; }

        public ICommand NavigateBackCommand { get; }
        public ICommand ArrivedOnLocationCommand { get; }
        public ICommand StartRideCommand { get; }
        public ICommand StopRideCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand ArrivedOnLocationLateCommand { get; }


        #region Property

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

        private Vehicle _vehicle;
        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {
                if (value != _vehicle)
                {
                    _vehicle = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public DriverRidesViewModel(User user, RidesService ridesService, FinishedRidesService finishedRidesService, VehicleService vehicleService, ModalNavigationStore modalNavigationStore)
        {

            _ridesService = ridesService;
            _finishedRidesService = finishedRidesService;
            _vehicleService = vehicleService;

            _ridesService.Subscribe(this);

            User = user;

            Vehicle = _vehicleService.GetVehicleByUserID(user.GetId());

            Rides = new ObservableCollection<Rides>(_ridesService.GetAll());
            ActiveRides = new ObservableCollection<Rides>(_ridesService.GetActiveRides(User, Vehicle));


            
            NavigateBackCommand = new NavigateBackCommand(CreateCloseRidesNavigationService(modalNavigationStore));

            ArrivedOnLocationCommand = new ArrivedCommand(this);
            StartRideCommand = new StartCommand(this);
            StopRideCommand = new StopCommand(this, _finishedRidesService, _ridesService);
            ArrivedOnLocationLateCommand = new LateArrivedCommand(this);
            CancelCommand = new CancelCommand(this, _ridesService);
        }

        private INavigationService CreateCloseRidesNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        public void Update()
        {
            ActiveRides.Clear();
            foreach(Rides activeRide in _ridesService.GetActiveRides(User, Vehicle))
            {
                ActiveRides.Add(activeRide);
            }
        }
    }
}
