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
    class CancelCommand : CommandBase
    {
        private readonly DriverRidesViewModel _viewModel;
        private readonly RidesService _ridesService;

        public CancelCommand(DriverRidesViewModel viewModel, RidesService ridesService)
        {
            _viewModel = viewModel;
            _ridesService = ridesService;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            MessageBox.Show("Ride successfully canceled!");

            _ridesService.Delete(_viewModel.SelectedRide);

            _viewModel.Vehicle.CanceledRidesCount++;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRide != null && string.IsNullOrEmpty(_viewModel.RemainingTime) && string.IsNullOrEmpty(_viewModel.Taximeter) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRidesViewModel.SelectedRide) || e.PropertyName == nameof(DriverRidesViewModel.Taximeter) || e.PropertyName == nameof(DriverRidesViewModel.RemainingTime))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
