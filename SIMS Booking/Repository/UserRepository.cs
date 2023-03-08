using SIMS_Booking.Model;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class UserRepository : Repository<User>
    {        
        public UserRepository() : base("../../../Resources/Data/users.csv") { }        

        public User GetByUsername(string username)
        {
            _entityList = _serializer.FromCSV(_filePath);
            return _entityList.FirstOrDefault(u => u.Username == username);
        }
    }
}
