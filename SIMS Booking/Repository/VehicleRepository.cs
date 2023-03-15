using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class VehicleRepository : Repository<Vehicle>, ISubject
    {        
        public VehicleRepository() : base("../../../Resources/Data/vehicles.csv") { } 
    }
}
