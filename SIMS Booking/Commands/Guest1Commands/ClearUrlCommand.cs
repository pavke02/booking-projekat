using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class ClearUrlCommand : CommandBase
    {
        private readonly Guest1OwnerReviewViewModel _viewModel;

        public ClearUrlCommand(Guest1OwnerReviewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.ImageURLs = "";
        }
    }
}
