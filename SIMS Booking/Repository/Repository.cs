using SIMS_Booking.Serializer;
using SIMS_Booking.State;
using System.Collections.Generic;

namespace SIMS_Booking.Repository
{
    public class Repository<T> where T: ISerializable, IDable, new()
    {
        protected readonly string _filePath;
        protected readonly Serializer<T> _serializer;
        protected List<T> _entityList;

        public Repository(string filePath)
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
            entity.setID(GetNextId(_entityList));
            _entityList.Add(entity);            
            _serializer.ToCSV(_filePath, _entityList);
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
    }
}
