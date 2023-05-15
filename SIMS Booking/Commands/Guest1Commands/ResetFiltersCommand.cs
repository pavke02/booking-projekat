using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.UI.ViewModel.Guest1;
using Microsoft.VisualStudio.Services.Profile;

namespace SIMS_Booking.Commands.Guest1Commands
{
    public class ResetFiltersCommand : CommandBase
    {
        private readonly Guest1MainViewModel _viewModel;

        public ResetFiltersCommand(Guest1MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.AccommodationName = "";
            _viewModel.Country = new KeyValuePair<string, List<string>>();
            _viewModel.City = "";
            _viewModel.HouseChecked = true;
            _viewModel.ApartmentChecked = true;
            _viewModel.CottageChecked = true;
            _viewModel.MaxGuests = "";
            _viewModel.MinReservationDays = "";
        }
    }
}
