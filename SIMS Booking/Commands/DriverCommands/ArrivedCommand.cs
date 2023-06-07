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
    class ArrivedCommand : CommandBase
    {
        private readonly DriverRidesViewModel _viewModel;
        private TimeSpan timeDif;
        private int remainingTime;

        public ArrivedCommand(DriverRidesViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {

            timeDif = _viewModel.SelectedRide.DateTime - DateTime.Now;
            if (timeDif.TotalSeconds <= 900)
            {
                remainingTime = (int)(20 * 60 + timeDif.TotalSeconds);

                _viewModel.timer = new DispatcherTimer();
                _viewModel.timer.Interval = TimeSpan.FromSeconds(1);
                _viewModel.timer.Tick += Timer_Tick;

                _viewModel.timer.Start();
            }
            else
            {
                MessageBox.Show("You arrived too soon!");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            _viewModel.RemainingTime = Convert.ToString(TimeSpan.FromSeconds(remainingTime));

            if (remainingTime == 0)
            {
                _viewModel.timer.Stop();
                MessageBox.Show("Guest is late!");
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.SelectedRide != null && string.IsNullOrEmpty(_viewModel.RemainingTime) && string.IsNullOrEmpty(_viewModel.Taximeter) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRidesViewModel.SelectedRide) || e.PropertyName == nameof(DriverRidesViewModel.Taximeter) || e.PropertyName == nameof(DriverRidesViewModel.RemainingTime))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
