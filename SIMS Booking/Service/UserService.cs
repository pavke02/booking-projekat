using SIMS_Booking.Model;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class UserService
    {
        private readonly ICRUDRepository<User> _repository;

        public UserService(ICRUDRepository<User> repository)
        {
            _repository = repository;
        }

        #region Crud

        public void Save(User user)
        {
            _repository.Save(user);
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(User user)
        {
            _repository.Update(user);
        }

        #endregion

        public User GetByUsername(string username)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Username == username);
        }

        public bool CheckPassword(User user, string pasword)
        {
            return user.Password == pasword;
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }

    }
}
