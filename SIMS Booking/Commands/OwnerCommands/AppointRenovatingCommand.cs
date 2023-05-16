using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Owner;
using System.ComponentModel;
using System.Windows;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.Commands.OwnerCommands;

public class AppointRenovatingCommand : CommandBase
{
    private readonly RenovationAppointmentService _renovationAppointmentService;
    private readonly AccommodationService _accommodationService;
    private readonly RenovationAppointingViewModel _viewModel;
    private readonly INavigationService _navigationService;
    private readonly Accommodation _accommodation;

    public AppointRenovatingCommand(RenovationAppointingViewModel viewModel, RenovationAppointmentService renovationAppointmentService, AccommodationService accommodationService, INavigationService navigationService, Accommodation accommodation)
    {
        _renovationAppointmentService = renovationAppointmentService;
        _accommodationService = accommodationService;
        _viewModel = viewModel;
        _accommodation = accommodation;

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        _navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        RenovationAppointment renovationAppointment = new RenovationAppointment(_viewModel.StartDate,
            _viewModel.EndDate, _viewModel.Description, true, _accommodationService.GetById(_accommodation.GetId()));
        _renovationAppointmentService.Save(renovationAppointment);
        MessageBox.Show("Renovation appointed successfully");

        _navigationService.Navigate();
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