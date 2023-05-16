using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.Windows;
using System.ComponentModel;
using SIMS_Booking.UI.ViewModel.Driver;
using System.Windows.Threading;

namespace SIMS_Booking.Commands.DriverCommands
{
    class StopCommand : CommandBase
    {
        private readonly DriverRidesViewModel _viewModel;
        private readonly FinishedRidesService _finishedRidesService;
        private readonly RidesService _ridesService;

        public StopCommand(DriverRidesViewModel viewModel, FinishedRidesService finishedRidesService, RidesService ridesService)
        {
            _viewModel = viewModel;
            _finishedRidesService = finishedRidesService;
            _ridesService = ridesService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.timer2.Stop();
            MessageBox.Show("Ride successfully finished!");

            FinishedRide selectedFinishedRide = new FinishedRide();
            selectedFinishedRide.Ride = _viewModel.SelectedRide;
            selectedFinishedRide.Price = _viewModel.Price;
            selectedFinishedRide.Time = _viewModel.Taximeter;
            selectedFinishedRide.Ride.DriverID = _viewModel.User.GetId();

            _viewModel.Price = "";
            _viewModel.Taximeter = "";

            _ridesService.Delete(_viewModel.SelectedRide);
            _finishedRidesService.Save(selectedFinishedRide);
        }
        public override bool CanExecute(object? parameter)
        {
            return string.IsNullOrEmpty(_viewModel.RemainingTime) && !string.IsNullOrEmpty(_viewModel.Taximeter) && !string.IsNullOrEmpty(_viewModel.Price) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRidesViewModel.SelectedRide) || e.PropertyName == nameof(DriverRidesViewModel.Taximeter) || e.PropertyName == nameof(DriverRidesViewModel.RemainingTime) || e.PropertyName == nameof(DriverRidesViewModel.LateInMinutes))
            {
                OnCanExecuteChanged();
            }
        }

    }
}
