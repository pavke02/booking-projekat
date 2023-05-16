using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Enums;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.Windows;
using System.ComponentModel;
using SIMS_Booking.UI.ViewModel.Driver;
using System.Windows.Threading;

namespace SIMS_Booking.Commands.DriverCommands
{
    class StartCommand : CommandBase
    {
        private readonly DriverRidesViewModel _viewModel;
        private int timerTickCounter;
        private int startPrice = 190;

        public StartCommand(DriverRidesViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            _viewModel.timer.Stop();
            _viewModel.RemainingTime = "";
            _viewModel.RSDString = "RSD";
            _viewModel.Price = Convert.ToString(startPrice);
            _viewModel.Taximeter = TimeSpan.Zero.ToString();

            _viewModel.timer2 = new DispatcherTimer();
            _viewModel.timer2.Interval = TimeSpan.FromSeconds(1);
            _viewModel.timer2.Tick += Timer_Tick;
            _viewModel.timer2.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerTickCounter++;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timerTickCounter);
            _viewModel.Taximeter = timeSpan.ToString(@"hh\:mm\:ss");
            startPrice += 2;
            _viewModel.Price = Convert.ToString(startPrice);
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.RemainingTime) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRidesViewModel.RemainingTime))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
