using SIMS_Booking.Model;

namespace SIMS_Booking.Repository
{
    public class UserRepository : Repository<User>
    {        
        public UserRepository() : base("../../../Resources/Data/users.csv") { }        
    }
}
