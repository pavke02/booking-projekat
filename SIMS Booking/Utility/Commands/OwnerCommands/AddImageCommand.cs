﻿using SIMS_Booking.UI.ViewModel;
using System;
using System.ComponentModel;
using System.Security.Policy;

namespace SIMS_Booking.Utility.Commands.OwnerCommands
{
    internal class AddImageCommand : CommandBase
    {
        private readonly OwnerMainViewModel _viewModel;

        public AddImageCommand(OwnerMainViewModel viewModel)
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
            return !string.IsNullOrEmpty(_viewModel.Url) && !string.IsNullOrWhiteSpace(_viewModel.Url) && Uri.IsWellFormedUriString(_viewModel.Url, UriKind.Absolute) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(OwnerMainViewModel.Url))
                OnCanExecuteChanged();
        }
    }
}