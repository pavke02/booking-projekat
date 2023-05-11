using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Repository
{
    public interface ICRUDRepository<T> : ISubject
    {
        public int GetNextId(List<T> etities);
        public List<T> GetAll();
        public void Save(T entity);
        public void Delete(T entity);
        public T Update(T entity);
        public T GetById(int id);
    }
}
