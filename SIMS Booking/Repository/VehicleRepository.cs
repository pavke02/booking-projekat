using SIMS_Booking.Model;

namespace SIMS_Booking.Repository
{
    public class VehicleRepository : Repository<Vehicle>
    {        
        public VehicleRepository() : base("../../../Resources/Data/vehicles.csv") { } 
    }
}
