using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Repository
{
    public class Repository<T> where T: ISerializable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;

        public Repository(string filePath)
        {
            _serializer = new Serializer<T>();  
            _entityList = new List<T>();
            _filePath = filePath;
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
