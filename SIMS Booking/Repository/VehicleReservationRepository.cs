using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class VehicleReservationRepository : Repository<VehicleReservation>
    {

        public VehicleReservationRepository() : base("../../../Resources/Data/vehiclereservation.csv") { }

    }
}
