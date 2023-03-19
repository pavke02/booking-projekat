using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class VehicleRepository : Repository<Vehicle>, ISubject
    {        
        public VehicleRepository() : base("../../../Resources/Data/vehicles.csv") { } 


        public Vehicle GetVehicleByUserID(int id)
        {
            return _entityList.Find(e => e.UserID == id);
        }
    }
}
