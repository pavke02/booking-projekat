using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class UserService
    {
        private readonly UserRepository _repository; 

        public UserService()
        {
            _repository = new UserRepository();
        }

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public User GetByUsername(string username)
        {            
            return _repository.GetAll().FirstOrDefault(u => u.Username == username);
        }
    }
}
