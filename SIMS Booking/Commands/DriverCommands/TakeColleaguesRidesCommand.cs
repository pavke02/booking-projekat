using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.UI.ViewModel.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_Booking.Commands.DriverCommands
{
    class TakeColleaguesRidesCommand : CommandBase
    {
        private readonly DriverProfileViewModel _viewModel;
        private readonly RidesService _ridesService;

        public TakeColleaguesRidesCommand(DriverProfileViewModel viewModel, RidesService ridesService)
        {
            _viewModel = viewModel;
            _ridesService = ridesService;
        }

        public override void Execute(object? parameter)
        {
            int count = 0;
            int count2 = 0;

            List<Rides> csvRides = new List<Rides>();
            csvRides = _ridesService.GetAll();

            List<Rides> newRides = new List<Rides>();

            foreach (Rides ride in _ridesService.GetAll())
            {
                if(ride.Pending == true)
                {
                    count++;
                }
            }

            foreach(Rides ride in _ridesService.GetAll())
            {
                if(ride.Pending == true && _viewModel.Vehicle.Locations.Any(e => e.City == ride.Location.City))
                {
                    count2 = 0;
                    foreach (Rides ridee in _ridesService.GetAll())
                    {
                        if (ridee.DriverID == _viewModel.Vehicle.UserID && ridee.DateTime == ride.DateTime)
                        {
                            count2 = 1;
                        }
                    }
                    if (count2 == 0)
                    {
                        count--;
                    }
                }    
            }

            if(count == 0)
            {
                foreach (Rides ride in _ridesService.GetAll())
                {
                    if (ride.Pending == false)
                    {
                        newRides.Add(ride);
                    }
                }
                foreach (Rides ride in _ridesService.GetAll())
                {
                    if (ride.Pending == true)
                    {
                        ride.Pending = false;
                        ride.DriverID = _viewModel.Vehicle.UserID;
                        newRides.Add(ride);
                    }
                }

                foreach (Rides ride in _ridesService.GetAll().Reverse<Rides>())
                {
                    _ridesService.Delete(ride);
                }
                foreach (Rides ride in newRides)
                {
                    _ridesService.Save(ride);
                }
                MessageBox.Show("Rides transfered!");
            }
            else
            {
                MessageBox.Show("You can not take colleagues rides!");
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _viewModel.CanTakeRides && base.CanExecute(parameter);
        }
    }
}
