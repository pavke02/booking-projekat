using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class RelationsCsvCrudRepository<T> where T : ISerializable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;
        protected List<IObserver> _observers;

        public RelationsCsvCrudRepository(string filePath)
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<T>();
            _filePath = filePath;
            _entityList = Load();
        }

        public List<T> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(T entity)
        {            
            _entityList.Add(entity);
            _serializer.ToCSV(_filePath, _entityList);           
        }

        public List<T> GetAll()
        {
            return _entityList;
        }
    }
}
