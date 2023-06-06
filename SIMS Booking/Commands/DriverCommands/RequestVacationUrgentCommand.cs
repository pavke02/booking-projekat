using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Booking.Commands.DriverCommands
{
    class RequestVacationUrgentCommand : CommandBase
    {
        private readonly DriverRequestVacationViewModel _viewModel;
        private readonly RidesService _ridesService;
        private readonly VehicleService _vehicleService;
        private readonly Vehicle Vehicle;
        List<Rides> csvRides = new List<Rides>();

        public RequestVacationUrgentCommand(DriverRequestVacationViewModel viewModel, VehicleService vehicleService, RidesService ridesService, Vehicle vehicle)
        {
            _viewModel = viewModel;
            _ridesService = ridesService;
            _vehicleService = vehicleService;
            Vehicle = vehicle;

            csvRides = _ridesService.GetAll();

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }



        public override void Execute(object? parameter)
        {
            foreach (Rides ride in _ridesService.GetAll())
            {
                if (ride.DriverID == Vehicle.UserID && ride.DateTime.Day >= _viewModel.StartingDate.Day && ride.DateTime.Day <= _viewModel.EndingDate.Day)
                {
                    _viewModel.DriverRides.Add(ride);
                }
            }
            List<Rides> newRides = new List<Rides>();
            int count = _viewModel.DriverRides.Count;
            int s = 0;
            int ss = 0;
            foreach(Rides ride in _viewModel.DriverRides)
            {
                foreach(Vehicle vehicle in _vehicleService.GetAll())
                {
                    if(ride.DriverID != vehicle.UserID && vehicle.Locations.Any(e => e.City == ride.Location.City) && ride.DateTime.Day >= _viewModel.StartingDate.Day && ride.DateTime.Day <= _viewModel.EndingDate.Day)
                    {
                        ss = 1;
                        foreach(Rides ridee in _ridesService.GetAll())
                        {
                            if(ridee.DriverID == vehicle.UserID && ridee.DateTime == ride.DateTime)
                            {
                                s = 1;
                            }
                        }
                    }
                }
            }

            if (ss == 0)
                s = 1;

            if(s == 0)
            {
                foreach(Rides ride in _viewModel.DriverRides)
                {
                    csvRides.Remove(ride);
                }
                foreach (Rides ride in _viewModel.DriverRides)
                {
                    foreach (Vehicle vehicle in _vehicleService.GetAll())
                    {
                        if (ride.DriverID != vehicle.UserID && vehicle.Locations.Any(e => e.City == ride.Location.City))
                        {
                            ride.DriverID = vehicle.UserID;
                            newRides.Add(ride);
                        }
                    }
                }
                foreach (Rides ridee in csvRides)
                {
                    newRides.Add(ridee);
                }
                foreach (Rides ride in csvRides.Reverse<Rides>())
                {
                    _ridesService.Delete(ride);
                }
                foreach (Rides ride in newRides)
                {
                    _ridesService.Save(ride);
                }
                _viewModel.Vehicle.StartDate = DateOnly.FromDateTime(_viewModel.StartingDate);
                _viewModel.Vehicle.EndDate = DateOnly.FromDateTime(_viewModel.EndingDate);
                _viewModel.Vehicle.OnVacation = "notpending";
                MessageBox.Show("Odmor odobren!");
            }
            else
            {
                MessageBox.Show("Nije moguce dobiti odmor!");
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.StartingDate < DateTime.Today.AddDays(2) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRequestVacationViewModel.StartingDate) || e.PropertyName == nameof(DriverRequestVacationViewModel.EndingDate))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
