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
    class LateArrivedCommand : CommandBase
    {
        private readonly DriverRidesViewModel _ridesViewModel;
        private TimeSpan timeDif;
        private int remainingTime;

        public LateArrivedCommand(DriverRidesViewModel ridesViewModel)
        {
            _ridesViewModel = ridesViewModel;

            _ridesViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            timeDif = _ridesViewModel.SelectedRide.DateTime - DateTime.Now;
            if (timeDif.TotalSeconds <= 900)
            {
                int lateTime = Convert.ToInt16(_ridesViewModel.LateInMinutes);
                remainingTime = (int)(20 * 60 + timeDif.TotalSeconds + 60 * lateTime);

                _ridesViewModel.timer = new DispatcherTimer();
                _ridesViewModel.timer.Interval = TimeSpan.FromSeconds(1);
                _ridesViewModel.timer.Tick += Timer_Tick;

                _ridesViewModel.timer.Start();
            }
            else
            {
                MessageBox.Show("You arrived too soon!");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTime--;

            _ridesViewModel.RemainingTime = Convert.ToString(TimeSpan.FromSeconds(remainingTime));

            if (remainingTime == 0)
            {
                _ridesViewModel.timer.Stop();
                MessageBox.Show("Guest is late!");
            }
        }
        public override bool CanExecute(object? parameter)
        {
            return _ridesViewModel.SelectedRide != null && string.IsNullOrEmpty(_ridesViewModel.RemainingTime) && string.IsNullOrEmpty(_ridesViewModel.Taximeter) && !string.IsNullOrEmpty(_ridesViewModel.LateInMinutes) && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DriverRidesViewModel.SelectedRide) || e.PropertyName == nameof(DriverRidesViewModel.Taximeter) || e.PropertyName == nameof(DriverRidesViewModel.RemainingTime) || e.PropertyName == nameof(DriverRidesViewModel.LateInMinutes))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
