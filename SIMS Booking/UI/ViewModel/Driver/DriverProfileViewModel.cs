﻿using SIMS_Booking.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Commands.DriverCommands;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverProfileViewModel : ViewModelBase
    {
        private FinishedRidesService _finishedRidesService;
        private VehicleService _vehicleService;
        public User User { get; set; }
        public static ObservableCollection<FinishedRide> FinishedRides { get; set; }
        public ICommand NavigateBackCommand { get; }

        //private List<FinishedRide> _finishedRides;
        //public List<FinishedRide> FinishedRides
        //{
        //    get => _finishedRides;
        //    set
        //    {
        //        if (value != _finishedRides)
        //        {
        //            _finishedRides = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private int _fastRidesCount;
        public int FastRidesCount
        {
            get => _fastRidesCount;
            set
            {
                if (value != _fastRidesCount)
                {
                    _fastRidesCount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                if (value != _status)
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _points;
        public int Points
        {
            get => _points;
            set
            {
                if (value != _points)
                {
                    _points = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _salary;
        public string Salary
        {
            get => _salary;
            set
            {
                if (value != _salary)
                {
                    _salary = value;
                    OnPropertyChanged();
                }
            }
        }

        private Vehicle _vehicle;
        public Vehicle Vehicle
        {
            get => _vehicle;
            set
            {
                if (value != _vehicle)
                {
                    _vehicle = value;
                    OnPropertyChanged();
                }
            }
        }

        public DriverProfileViewModel(ModalNavigationStore modalNavigationStore, FinishedRidesService finishedRidesService, VehicleService vehicleService, User user)
        {
            _finishedRidesService = finishedRidesService;
            _vehicleService = vehicleService;
            User = user;

            Vehicle = _vehicleService.GetVehicleByUserID(User.getID());

            FinishedRides = new ObservableCollection<FinishedRide>(_finishedRidesService.GetAll());
            FastRidesCount = 0;
            Points = 0;
            Salary = "";

            foreach (FinishedRide finishedRide in FinishedRides)
            {
                if (finishedRide.Ride.Type == "Fast" && finishedRide.Ride.DriverID == User.getID())
                {
                    FastRidesCount++;
                }
            }

            if(FastRidesCount >= 15)
            {
                Status = "Super Driver";
                Points = (FastRidesCount - 15) * 5 - Vehicle.CanceledRidesCount * 2;
                FastRidesCount = 15;
                if(FastRidesCount < 50)
                {
                    Salary = "You need " + (50 - Points) + " more points for extra salary!";
                }
                else
                {
                    Salary = "You have extra salary!";
                }
            }
            else
            {
                Status = "Regular Driver";
            }

            NavigateBackCommand = new NavigateBackCommand(CreateCloseProfileNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseProfileNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
    }
    
}
