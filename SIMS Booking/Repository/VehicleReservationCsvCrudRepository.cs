using SIMS_Booking.Model.Relations;

namespace SIMS_Booking.Repository
{
    public class VehicleReservationCsvCrudRepository : CsvCrudRepository<ReservationOfVehicle>
    {

        public VehicleReservationCsvCrudRepository() : base("../../../Resources/Data/vehiclereservation.csv") { }

    }
}
