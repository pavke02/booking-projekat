using SIMS_Booking.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository.RelationsRepository
{
    internal class RelationsRepository<T> where T : ISerializable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;

        public RelationsRepository(string filePath)
        {            
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
    }
}
