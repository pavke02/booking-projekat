using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;
using System.Collections.Generic;

namespace SIMS_Booking.Service.RelationsService
{
    internal class RelationsCrudService<T> where T: ISerializable,IDable, new()
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
