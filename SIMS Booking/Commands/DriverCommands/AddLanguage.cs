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
    class AddLanguage : CommandBase
    {
        private readonly DriverAddVehicleViewModel _viewModel;

        public AddLanguage(DriverAddVehicleViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.Languages += _viewModel.SelectedLanguage + "\n";
            _viewModel.SelectedLanguage = null;
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedLanguage != null && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverAddVehicleViewModel.SelectedLanguage))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
