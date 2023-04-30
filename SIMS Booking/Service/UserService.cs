using SIMS_Booking.Model;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class UserService
    {
        private readonly CrudService<User> _crudService;

        public UserService()
        {
            _crudService = new CrudService<User>("../../../Resources/Data/users.csv");
        }

        #region Crud

        public void Save(User user)
        {
            _crudService.Save(user);
        }

        public User GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Update(User user)
        {
            _crudService.Update(user);
        }

        #endregion

        public User GetByUsername(string username)
        {
            return _crudService.GetAll().FirstOrDefault(u => u.Username == username);
        }

        public bool CheckPassword(User user, string pasword)
        {
            return user.Password == pasword;
        }

        public List<User> GetAll()
        {
            return _crudService.GetAll();
        }
    }
}
