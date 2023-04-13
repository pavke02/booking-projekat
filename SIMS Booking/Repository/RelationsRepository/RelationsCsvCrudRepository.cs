using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class RelationsCsvCrudRepository<T> where T : ISerializable, IDable, new()
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

        public T? Update(T entity)
        {
            T? current = _entityList.Find(c => c.getID() == entity.getID());
            if (current == null) return default(T);
            int index = _entityList.IndexOf(current);
            _entityList.Remove(current);
            _entityList.Insert(index, entity);       // keep ascending order of ids in file 
            _serializer.ToCSV(_filePath, _entityList);
            NotifyObservers();
            return entity;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }


    }
}
