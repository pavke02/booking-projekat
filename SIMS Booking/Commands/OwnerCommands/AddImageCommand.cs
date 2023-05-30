using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.ComponentModel;

namespace SIMS_Booking.Commands.OwnerCommands
{
    internal class AddImageCommand : CommandBase
    {
        private readonly IPublish _viewModel;

        public AddImageCommand(IPublish viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            if (string.IsNullOrEmpty(_viewModel.ImageURLs))
                _viewModel.ImageURLs = _viewModel.Url;
            else
                _viewModel.ImageURLs = _viewModel.ImageURLs + "\n" + _viewModel.Url;

            _viewModel.Url = "";
        }

        public override bool CanExecute(object? parameter)
        {
            return Uri.IsWellFormedUriString(_viewModel.Url, UriKind.Absolute) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OwnerMainViewModel.Url))
            {
                _viewModel.ButtCancel = Uri.IsWellFormedUriString(_viewModel.Url, UriKind.Absolute);
                OnCanExecuteChanged();
            }
        }
    }
}
