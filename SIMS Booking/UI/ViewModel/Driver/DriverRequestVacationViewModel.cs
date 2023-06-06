using SIMS_Booking.Commands.DriverCommands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverRequestVacationViewModel : ViewModelBase
    {
        private VehicleService _vehicleService;
        private RidesService _ridesService;
        public Vehicle Vehicle { get; set; }
        public ICommand NavigateBackCommand { get; }
        public ICommand RequestVacationCommand { get; }
        public ICommand RequestVacationUrgentCommand { get; }

        private DateTime _startingDate;
        public DateTime StartingDate
        {
            get => _startingDate;
            set
            {
                _startingDate = value;
                if(StartingDate >= EndingDate)
                {
                    EndingDate = StartingDate.AddDays(1);
                }
                OnPropertyChanged();
            }
        }
        private DateTime _endingDate;
        public DateTime EndingDate
        {
            get => _endingDate;
            set
            {
                if (value <= StartingDate)
                {
                    _endingDate = StartingDate.AddDays(1);
                }
                else
                {
                    _endingDate = value;
                }
                OnPropertyChanged();
            }
        }

        private List<Rides> _driverRides;
        public List<Rides> DriverRides
        {
            get => _driverRides;
            set
            {
                _driverRides = value;
                OnPropertyChanged();
            }
        }

        public DriverRequestVacationViewModel(Vehicle vehicle, ModalNavigationStore modalNavigationStore, VehicleService vehicleService, RidesService ridesService)
        {
            Vehicle = vehicle;
            _vehicleService = vehicleService;
            _ridesService = ridesService;

            DriverRides = new List<Rides>();

            //foreach(Rides ride in _ridesService.GetAll())
            //{
            //    if(ride.DriverID == Vehicle.UserID && ride.DateTime.Day >= StartingDate.Day && ride.DateTime.Day <= EndingDate.Day)
            //    {
            //        DriverRides.Add(ride);
            //    }
            //}

            StartingDate = DateTime.Today.AddDays(1);
            EndingDate = DateTime.Today.AddDays(2);

            NavigateBackCommand = new NavigateBackCommand(CreateCloseDriverRequestVacationService(modalNavigationStore));

            RequestVacationCommand = new RequestVacationCommand(this, _vehicleService, _ridesService, Vehicle);
            RequestVacationUrgentCommand = new RequestVacationUrgentCommand(this, _vehicleService, _ridesService, Vehicle);
        }

        private INavigationService CreateCloseDriverRequestVacationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
}
