using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.ViewModel.Guest2;
using System;

namespace SIMS_Booking.Commands.Guest2Commands
{
    public class DrivingReservationCommand : CommandBase
    {
        private readonly DrivingReservationViewModel _viewModel;
        private readonly VehicleReservationService _vehicleReservationService;
        private readonly ReservationOfVehicle  _vehicleReservation;
        private readonly INavigationService _closeModalNavigationService;

        public DrivingReservationCommand(INavigationService closeModalNavigationService, DrivingReservationViewModel viewModel, VehicleReservationService vehicleReservationService, ReservationOfVehicle vehicleReservation)
        {
            _viewModel = viewModel;
            _closeModalNavigationService = closeModalNavigationService;
            

            _vehicleReservationService = vehicleReservationService; 
            _vehicleReservation = vehicleReservation;


        }

        public override void Execute(object? parameter)
        {

            _viewModel.ErrorText = string.IsNullOrEmpty(_viewModel.Comment);
            if (_viewModel.ErrorText) return;

            _vehicleReservationService.SubmitDriveReservation(_viewModel.UserId,_viewModel.VehicleId, _viewModel.Time, _viewModel.Address, _viewModel.Destination);
            _vehicleReservationService.Update(_vehicleReservation);

            _closeModalNavigationService.Navigate();


        }

       
    }
}
