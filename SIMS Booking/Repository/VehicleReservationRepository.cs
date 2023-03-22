using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using System;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class VehicleReservationRepository : Repository<VehicleReservation>
    {

        public VehicleReservationRepository() : base("../../../Resources/Data/vehiclereservation.csv") { }

        internal void Save(ReservationOfVehicle reservedVehicle)
        {
            throw new NotImplementedException();
        }
    }
}
