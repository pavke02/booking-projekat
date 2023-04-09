using SIMS_Booking.Repository;
using SIMS_Booking.Utility;
using SIMS_Booking.Serializer;
using System.Collections.Generic;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Service
{
    public class CrudService<T> where T: ISerializable, IDable, new()
    {
        private readonly CsvCrudRepository<T> _csvCrudRepository;

        public CrudService(string filePath)
        {
            _csvCrudRepository = new CsvCrudRepository<T>(filePath);
        }

        public void Save(T entity)
        {
            _csvCrudRepository.Save(entity);
        }

        public T? Update(T entity)
        {
            return _csvCrudRepository.Update(entity);
        }

        public void Delete(T entity)
        {
            _csvCrudRepository.Delete(entity);
        }

        public List<T> GetAll()
        {
            return _csvCrudRepository.GetAll();
        }

        public T GetById(int id)
        {
            return _csvCrudRepository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _csvCrudRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _csvCrudRepository.Unsubscribe(observer);
        }
    }
}
