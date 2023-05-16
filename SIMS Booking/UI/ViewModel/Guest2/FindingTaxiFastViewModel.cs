using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Windows;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Guest2
{
    public class FindingTaxiFastViewModel : ViewModelBase
    {
        public VehicleReservationService _vehicleReservationService;
        public DriverLocationsService _driverLocationsService;
        public User loggedUser { get; set; }

        public ICommand SubmitReviewCommand { get; }
        //ToDo: implementirati NavigateBackCommand
        public ICommand NavigateBackCommand { get; }

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

        private int tidiness = 0;
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    if (value > 5)
                        tidiness = 5;
                    else if (value < 1)
                        tidiness = 1;
                    else
                        tidiness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ruleFollowing = 0;
        public int RuleFollowing
        {
            get => ruleFollowing;
            set
            {
                if (value != ruleFollowing)
                {
                    if (value > 5)
                        ruleFollowing = 5;
                    else if (value < 1)
                        ruleFollowing = 1;
                    else
                        ruleFollowing = value;
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

        public FindingTaxiFastViewModel(User _loggedUser, VehicleReservationService vehicleReservationService, DriverLocationsService driverLocationsService)
        {
            loggedUser = _loggedUser;

            _vehicleReservationService = vehicleReservationService;
            _driverLocationsService = driverLocationsService;
        }

        public bool Button_Click(string city, string startingAddress, string finalAddress, string timeOfDeparture)
        {
            if (city != "" && startingAddress != "" && finalAddress != "" && timeOfDeparture != "")
            {
                DriverLocations driverLocations = _driverLocationsService.GetDriverLocationsByLocation(city);
                if (driverLocations == null)
                {
                    MessageBox.Show("Trenutno nema slobodnih vozila. ");
                }
                else
                {
                    _vehicleReservationService.Save(new ReservationOfVehicle(loggedUser.GetId(), driverLocations.DriverId, timeOfDeparture, new Address(startingAddress, driverLocations.Location), new Address(finalAddress, driverLocations.Location)));
                    MessageBox.Show("Uspesno ste rezervisali brzu voznju. ");
                    return true;
                }
            }
            return false;
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
