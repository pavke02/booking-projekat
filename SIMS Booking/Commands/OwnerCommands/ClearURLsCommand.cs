﻿using SIMS_Booking.UI.ViewModel.Owner;
using System;

namespace SIMS_Booking.Commands.OwnerCommands
{
    internal class ClearURLsCommand : CommandBase
    {
        private readonly OwnerMainViewModel _viewModel;

        public ClearURLsCommand(OwnerMainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ImageURLs = "";
        }
    }
}
