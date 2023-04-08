using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class VehicleCsvCrudRepository : CsvCrudRepository<Vehicle>, ISubject
    {        
        public VehicleCsvCrudRepository() : base("../../../Resources/Data/vehicles.csv") { } 


        public Vehicle GetVehicleByUserID(int id)
        {
            return _entityList.Find(e => e.UserID == id);
        }
    }
}
