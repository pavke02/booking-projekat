using System;
using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Commands.OwnerCommands;

public class CancelRenovationCommand : CommandBase
{
    private readonly OwnerMainViewModel _viewModel;
    private readonly RenovationAppointmentService _renovationAppointingService;

    public CancelRenovationCommand(OwnerMainViewModel ownerMainViewModel, RenovationAppointmentService renovationAppointmentService)
    {
        _viewModel = ownerMainViewModel;
        _renovationAppointingService = renovationAppointmentService;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    public override void Execute(object? parameter)
    {
        MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to cancel renovation?", "Cancel Renovation Appointment", MessageBoxButton.YesNo);
        if ( messageBoxResult == MessageBoxResult.Yes)
        {
            _renovationAppointingService.Delete(_renovationAppointingService.GetById(_viewModel.SelectedRenovation.GetId()));
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return  _viewModel.SelectedRenovation != null && ((_viewModel.SelectedRenovation.StartDate - DateTime.Now).Days) >= 5 && base.CanExecute(parameter);
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OwnerMainViewModel.SelectedRenovation))
            OnCanExecuteChanged();
    }
}