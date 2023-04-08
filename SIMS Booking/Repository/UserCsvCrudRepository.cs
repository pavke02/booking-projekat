using SIMS_Booking.Model;

namespace SIMS_Booking.Repository
{
    public class UserCsvCrudRepository : CsvCrudRepository<User>
    {        
        public UserCsvCrudRepository() : base("../../../Resources/Data/users.csv") { }        
    }
}
