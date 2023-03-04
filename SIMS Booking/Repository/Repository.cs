using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;
using System;
using System.Collections.Generic;

namespace SIMS_Booking.Repository
{
    public class Repository<T> where T : ISerializable, IDable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;
        protected List<IObserver> _observers;

        public Repository(string filePath)
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
            entity.setID(GetNextId(_entityList));
            _entityList.Add(entity);            
            _serializer.ToCSV(_filePath, _entityList);
            NotifyObservers();
        }

        public List<T> GetAll()
        {
            Load();
            return _entityList;
        }

        public int GetNextId(List<T> etities) 
        {
            if (_entityList.Count == 0)
            {
                return 1;
            }

            int maxi = _entityList[0].getID();
            foreach (T entity in etities)
            {
                if (maxi < entity.getID())
                {
                    maxi = entity.getID();
                }
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
