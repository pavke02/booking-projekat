using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
