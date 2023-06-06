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
    class RequestVacationCommand : CommandBase
    {
        private readonly DriverRequestVacationViewModel _viewModel;
        private readonly RidesService _ridesService;
        private readonly VehicleService _vehicleService;
        private readonly Vehicle Vehicle;

        public RequestVacationCommand(DriverRequestVacationViewModel viewModel, VehicleService vehicleService, RidesService ridesService, Vehicle vehicle)
        {
            _viewModel = viewModel;
            _vehicleService = vehicleService;
            _ridesService = ridesService;


            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
            Vehicle = vehicle;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Vehicle.OnVacation = "pending";
            foreach(Rides ride in _ridesService.GetAll().Reverse<Rides>())
            {
                if(ride.DriverID == _viewModel.Vehicle.UserID)
                {
                    _ridesService.Delete(ride);
                    ride.Pending = true;
                    _ridesService.Save(ride);
                }
            }
            MessageBox.Show("Pending vacation");
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.StartingDate >= DateTime.Today.AddDays(2) && base.CanExecute(parameter);
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
