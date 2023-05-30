using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.ComponentModel;
using SIMS_Booking.UI.ViewModel.Guide;

namespace SIMS_Booking.Commands.NavigateCommands
{
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly ViewModelBase _viewModel;
        private readonly Func<bool> _canExecuteCondition;
      
        //ToDo: napraviti naslednika navigateCommand sa disable condition
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public NavigateCommand(INavigationService navigationService, ViewModelBase viewModel, Func<bool> canExecuteCondition)
        {
            _navigationService = navigationService;
            _viewModel = viewModel;
            _canExecuteCondition = canExecuteCondition;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            if (_canExecuteCondition == null) return base.CanExecute(parameter);

            return _canExecuteCondition() && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OwnerMainViewModel.SelectedReservation) || e.PropertyName == nameof(OwnerMainViewModel.SelectedReview) || 
                e.PropertyName == nameof(OwnerMainViewModel.SelectedAccommodation) || e.PropertyName==nameof(GuideMainViewModel.SelectedTour) ||
                e.PropertyName == nameof(LocationPopularityViewModel.SelectedPopularLocation) || e.PropertyName == nameof(LocationPopularityViewModel.SelectedUnpopularLocation))

                OnCanExecuteChanged();
        }
    }
}
