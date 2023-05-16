using SIMS_Booking.Commands.Guest2Commands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public class DrivingReservationViewModel : ViewModelBase
    {
        private DriverLocationsService _driverLocationsService;
        private VehicleReservationService _vehicleReservationService;
        private ReservationOfVehicle _vehicleReservation;
        private  DrivingReservationViewModel _drivingReservationViewModel;
      
        #region Property
        private bool _errorText;
        public bool ErrorText
        {
            get => _errorText;
            set
            {
                if (value != _errorText)
                {
                    _errorText = value;
                    OnPropertyChanged();
                }
            }
        }

        private Address address;
        public Address Address
        {
            get => address;
            set
            {
                if (value != address)
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }

        private Address destination;
        public Address Destination
        {
            get => destination;
            set
            {
                if (value != destination)
                {
                    destination = value;
                    OnPropertyChanged();
                }
            }
        }



        private int vehicleId = 0;
        public int VehicleId
        {
            get => vehicleId;
            set
            {
                if (value != vehicleId)
                {
                    if (value > 5)
                        vehicleId = 5;
                    else if (value < 1)
                        vehicleId = 1;
                    else
                        vehicleId = value;
                    OnPropertyChanged();
                }
            }
        }

        private int userId = 0;
        public int UserId
        {
            get => userId;
            set
            {
                if (value != userId)
                {
                    if (value > 5)
                        userId = 5;
                    else if (value < 1)
                        userId = 1;
                    else
                        userId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string time;
        public string Time
        {
            get => time;
            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }

        private string comment;
        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public ICommand DrivingReservationCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public DrivingReservationViewModel(VehicleReservationService vehicleReservationService, ReservationOfVehicle vehicleReservation,ModalNavigationStore modalNavigationStore) {

            _vehicleReservationService = vehicleReservationService;
            _vehicleReservation = vehicleReservation;
            
            DrivingReservationCommand = new DrivingReservationCommand(CreateCloseModalNavigationService(modalNavigationStore),this,_vehicleReservationService,_vehicleReservation);
            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }
        #region Validation
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Comment")
                {
                    if (string.IsNullOrEmpty(Comment))
                        return "Required";
                }
                return null;
            }
        }
#endregion
    }
}
