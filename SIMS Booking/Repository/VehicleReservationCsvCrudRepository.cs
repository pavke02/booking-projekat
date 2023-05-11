using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using System;

namespace SIMS_Booking.Repository
{
    public class VehicleReservationCsvCrudRepository : CsvCrudRepository<ReservationOfVehicle>
    {

        public VehicleReservationCsvCrudRepository() : base() { }

    }
}
