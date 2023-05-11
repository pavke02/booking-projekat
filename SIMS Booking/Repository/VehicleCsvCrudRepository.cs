using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Repository
{
    public class VehicleCsvCrudRepository : CsvCrudRepository<Vehicle>, ISubject
    {        
        public VehicleCsvCrudRepository() : base() { } 


        public Vehicle GetVehicleByUserID(int id)
        {
            return _entityList.Find(e => e.UserID == id);
        }
    }
}
