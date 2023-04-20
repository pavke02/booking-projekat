using System;
using System.ComponentModel;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.ViewModel;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.Utility.Commands.NavigateCommands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly ViewModelBase _viewModel;
        private readonly Func<bool> _condition;

        public NavigateCommand(INavigationService navigationService, ViewModelBase viewModel = null, Func<bool> condition = null)
        {
            _viewModel = viewModel;
            _navigationService = navigationService;
            _condition = condition;

            //Question : da li je ovo okej, i da li bi moglo bolje da se uradi
            if (_viewModel != null)
                _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            if (_condition == null) return base.CanExecute(parameter);

            return _condition() && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OwnerMainViewModel.SelectedReservation) || e.PropertyName == nameof(OwnerMainViewModel.SelectedReview))
                OnCanExecuteChanged();
        }
    }
}
