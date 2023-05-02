using System;
using System.ComponentModel;
using System.Globalization;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands;

public class AppointRenovatingCommand : CommandBase
{
    private readonly RenovationAppointmentService _renovationAppointmentService;
    private readonly RenovationAppointingViewModel _viewModel;

    public AppointRenovatingCommand(RenovationAppointingViewModel viewModel, RenovationAppointmentService renovationAppointmentService)
    {
        _renovationAppointmentService = renovationAppointmentService;
        _viewModel = viewModel;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override void Execute(object? parameter)
    {
        RenovationAppointment renovationAppointment = new RenovationAppointment(DateTime.Parse(_viewModel.StartDate),
            DateTime.Parse(_viewModel.EndDate), _viewModel.Description, true);
        _renovationAppointmentService.Save(renovationAppointment);
    }

    public override bool CanExecute(object? parameter)
    {
        return _viewModel.StartDate != null && _viewModel.EndDate != null &&
                _viewModel.Description != null && base.CanExecute(parameter);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(RenovationAppointingViewModel.StartDate) || e.PropertyName == nameof(RenovationAppointingViewModel.EndDate) ||
            e.PropertyName == nameof(RenovationAppointingViewModel.Description))
        {
            OnCanExecuteChanged();
        }
    }
}