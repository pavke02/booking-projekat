using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.UI.ViewModel
{
    public class Guest2FindingTaxiFastViewModel
    {

        public VehicleReservationService vehicleReservationService;
        public DriverLocationService driverLocationService;
        public User loggedUser { get; set; }

        public Guest2FindingTaxiFastViewModel(User _loggedUser)
        {

            loggedUser = _loggedUser;

            vehicleReservationService = new VehicleReservationService();
            driverLocationService = new DriverLocationService();



        }

        public bool Button_Click(string city, string startingAddress, string finalAddress, string timeOfDeparture)
        {
            if (city != "" && startingAddress != "" && finalAddress != "" && timeOfDeparture != "")
            {
                DriverLocations driverLocations = driverLocationService.GetDriverLocationsByLocation(city);
                if (driverLocations == null)
                {
                    MessageBox.Show("Trenutno nema slobodnih vozila. ");
                }
                else
                {
                    vehicleReservationService.Save(new ReservationOfVehicle(loggedUser.getID(), driverLocations.DriverId, timeOfDeparture, new Address(startingAddress, driverLocations.Location), new Address(finalAddress, driverLocations.Location)));
                    MessageBox.Show("Uspesno ste rezervisali brzu voznju. ");
                    return true;
                }
            }
            return false;


        }
    }
}
