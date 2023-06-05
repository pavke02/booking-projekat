using PdfSharp.Drawing;
using SIMS_Booking.UI.ViewModel.Guest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Booking.Commands.Guest1Commands
{
    internal class MainDemoCommand : CommandBase
    {
        private readonly Guest1MainViewModel _viewModel;

        public MainDemoCommand(Guest1MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.AccommodationName = "";
            _viewModel.CountryIndex = -1;
            _viewModel.CityIndex = -1;
            _viewModel.HouseChecked = true;
            _viewModel.ApartmentChecked = true;
            _viewModel.CottageChecked = true;
            _viewModel.MaxGuests = "";
            _viewModel.MinReservationDays = "";

            _viewModel.CountryIndex = 2;
            System.Threading.Thread.Sleep(1000);
            _viewModel.CityIndex = 2;
            System.Threading.Thread.Sleep(1000);
            _viewModel.HouseChecked = false;
            System.Threading.Thread.Sleep(1000);
            _viewModel.CottageChecked = false;
            System.Threading.Thread.Sleep(1000);
            _viewModel.MaxGuests = "2";
            System.Threading.Thread.Sleep(1000);
            _viewModel.MinReservationDays = "1";
            System.Threading.Thread.Sleep(300);
            _viewModel.MinReservationDays = "10";
            System.Threading.Thread.Sleep(1000);
            _viewModel.AccommodationName = "H";
            System.Threading.Thread.Sleep(300);
            _viewModel.AccommodationName = "Ho";
            System.Threading.Thread.Sleep(300);
            _viewModel.AccommodationName = "Hot";
            System.Threading.Thread.Sleep(300);
            _viewModel.AccommodationName = "Hote";
            System.Threading.Thread.Sleep(300);
        }
    }
}
