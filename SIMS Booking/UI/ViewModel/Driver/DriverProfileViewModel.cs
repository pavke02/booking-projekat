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
    public class DriverProfileViewModel : ViewModelBase
    {
        private FinishedRidesService _finishedRidesService;
        private VehicleService _vehicleService;
        private RidesService _ridesService;
        public User User { get; set; }
        public static ObservableCollection<FinishedRide> FinishedRides { get; set; }
        public ICommand NavigateBackCommand { get; }
        public ICommand TakeColleaguesRidesCommand { get; }
        public ICommand GeneratePDFCommand { get; }

        private int _fastRidesCount;
        public int FastRidesCount
        {
            get => _fastRidesCount;
            set
            {
                if (value != _fastRidesCount)
                {
                    _fastRidesCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _points;
        public int Points
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

        private string _salary;
        public string Salary
        {
            get => _salary;
            set
            {
                if (value != _salary)
                {
                    _salary = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _mostPopularLocation;
        public string MostPopularLocation
        {
            get => _mostPopularLocation;
            set
            {
                if (value != _mostPopularLocation)
                {
                    _mostPopularLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _leastPopularLocation;
        public string LeastPopularLocation
        {
            get => _leastPopularLocation;
            set
            {
                if (value != _leastPopularLocation)
                {
                    _leastPopularLocation = value;
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

        private bool _canTakeRides;
        public bool CanTakeRides
        {
            get => _canTakeRides;
            set
            {
                if (value != _canTakeRides)
                {
                    _canTakeRides = value;
                    OnPropertyChanged();
                }
            }
        }

        public DriverProfileViewModel(ModalNavigationStore modalNavigationStore, FinishedRidesService finishedRidesService, VehicleService vehicleService, RidesService ridesService, User user)
        {
            _finishedRidesService = finishedRidesService;
            _vehicleService = vehicleService;
            _ridesService = ridesService;
            User = user;

            Vehicle = _vehicleService.GetVehicleByUserID(User.GetId());

            FinishedRides = new ObservableCollection<FinishedRide>(_finishedRidesService.GetAll());
            FastRidesCount = 0;
            Points = 0;
            Salary = "";
            MostPopularLocation = "";
            LeastPopularLocation = "";

            CanTakeRides = false;
            foreach (Rides ride in _ridesService.GetAll())
            {
                if(ride.Pending == true)
                {
                    CanTakeRides = true;
                    break;
                }
            }    

            Dictionary<string, int> locationCounts = new Dictionary<string, int>();

            foreach (FinishedRide finishedRide in FinishedRides)
            {
                string locationKey = finishedRide.Ride.Location.ToString();
                if (locationCounts.ContainsKey(locationKey))
                {
                    locationCounts[locationKey]++;
                }
                else
                {
                    locationCounts[locationKey] = 1;
                }
            }

            KeyValuePair<string, int> mostPopularLocation = locationCounts.OrderByDescending(x => x.Value).First();
            KeyValuePair<string, int> leastPopularLocation = locationCounts.OrderBy(x => x.Value).First();

            foreach(Location location in Vehicle.Locations)
            {
                string mostSplit = mostPopularLocation.Key.Split(',')[0];
                if (mostSplit == location.City)
                {
                    MostPopularLocation = "You vehicle is on most popular location! (" + location.Country + ", " + location.City + ")";
                }
            }

            foreach (Location location in Vehicle.Locations)
            {
                string leastSplit = leastPopularLocation.Key.Split(',')[0];
                if (leastSplit == location.City)
                {
                    LeastPopularLocation = "You vehicle is on least popular location! (" + location.Country + ", " + location.City + ")";
                }
            }

            foreach (FinishedRide finishedRide in FinishedRides)
            {
                if (finishedRide.Ride.Type == "Fast" && finishedRide.Ride.DriverID == User.GetId())
                {
                    FastRidesCount++;
                }
            }

            if(FastRidesCount >= 15)
            {
                Status = "Super Driver";
                Points = (FastRidesCount - 15) * 5 - Vehicle.CanceledRidesCount * 5;
                FastRidesCount = 15;
                if(Points < 50)
                {
                    Salary = "You need " + (50 - Points) + " more points for extra salary!";
                }
                else
                {
                    Salary = "You have extra salary!";
                }
            }
            else
            {
                Status = "Regular Driver";
            }



            NavigateBackCommand = new NavigateBackCommand(CreateCloseProfileNavigationService(modalNavigationStore));

            TakeColleaguesRidesCommand = new TakeColleaguesRidesCommand(this, _ridesService);
            GeneratePDFCommand = new DriverGeneratePDF(_finishedRidesService);
        }

        private INavigationService CreateCloseProfileNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }


    }
    
}
