using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.TeamFoundation.Common;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class AddImageOwnerReviewCommand : CommandBase
    {
        private readonly Guest1OwnerReviewViewModel _viewModel;

        public AddImageOwnerReviewCommand(Guest1OwnerReviewViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.ImageURLs.IsNullOrEmpty())
                _viewModel.ImageURLs = _viewModel.UrlText;
            else
                _viewModel.ImageURLs += "\n" + _viewModel.UrlText;

            _viewModel.UrlText = "";
        }
    }
}
