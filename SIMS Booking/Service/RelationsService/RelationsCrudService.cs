using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Service.RelationsService
{
    internal class RelationsCrudService<T> where T: ISerializable, new()
    {
        private readonly RelationsCsvCrudRepository<T> _relationsCsvCrudRepository;

        public RelationsCrudService(string filePath)
        {
            _relationsCsvCrudRepository = new RelationsCsvCrudRepository<T>(filePath);
        }

        public void Save(T entity)
        {
            _relationsCsvCrudRepository.Save(entity);
        }

        public List<T> GetAll()
        {
            return _relationsCsvCrudRepository.GetAll();
        }

    }
}
