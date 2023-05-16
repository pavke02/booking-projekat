using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class CsvCrudRepository<T> : ICRUDRepository<T> where T : ISerializable, IDable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;
        protected List<IObserver> _observers;

        public CsvCrudRepository()
        {
            _observers = new List<IObserver>();
            _serializer = new Serializer<T>();
            string fileName = typeof(T).Name[0].ToString().ToLower() + typeof(T).Name.Substring(1);
            _filePath = $"../../../Resources/Data/{fileName}.csv";
            _entityList = Load();
        }

        private List<T> Load()
        {
            return _serializer.FromCSV(_filePath);
        }

        public void Save(T entity)
        {
            entity.SetId(GetNextId(_entityList));
            _entityList.Add(entity);
            _serializer.ToCSV(_filePath, _entityList);
            NotifyObservers();
        }

        public T? Update(T entity)
        {            
            T? current = _entityList.Find(c => c.GetId() == entity.GetId());
            if (current == null) return default(T);
            int index = _entityList.IndexOf(current);
            _entityList.Remove(current);
            _entityList.Insert(index, entity);       // keep ascending order of ids in file 
            _serializer.ToCSV(_filePath, _entityList);
            NotifyObservers();
            return entity;
        }

        public void Delete(T entity)
        {
            T? foundEntity = _entityList.Find(c => c.GetId() == entity.GetId());
            if (foundEntity == null) return;
            _entityList.Remove(foundEntity);
            _serializer.ToCSV(_filePath, _entityList);
            NotifyObservers();
        }

        public List<T> GetAll()
        {
            return _entityList;
        }

        public T GetById(int id)
        {
            return _entityList.FirstOrDefault(e => e.GetId() == id);
        }

        public int GetNextId(List<T> etities)
        {
            if (_entityList.Count == 0)
            {
                return 1;
            }

            int maxi = _entityList[0].GetId();
            foreach (T entity in etities)
            {
                if (maxi < entity.GetId())                
                    maxi = entity.GetId();                
            }
            return maxi + 1;
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