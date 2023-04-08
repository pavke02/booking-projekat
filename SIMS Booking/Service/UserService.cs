using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class UserService
    {
        private readonly UserCsvCrudRepository _csvCrudRepository; 

        public UserService()
        {
            _csvCrudRepository = new UserCsvCrudRepository();
        }

        public void Save(User user)
        {
            _csvCrudRepository.Save(user);
        }

        public User GetById(int id)
        {
            return _csvCrudRepository.GetById(id);
        }

        public void Update(User user)
        {
            _csvCrudRepository.Update(user);
        }

        public User GetByUsername(string username)
        {            
            return _csvCrudRepository.GetAll().FirstOrDefault(u => u.Username == username);
        }
    }
}
