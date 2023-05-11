using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System.Collections.Generic;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service.RelationsService
{
    internal class RelationsCrudService<T> where T: ISerializable,IDable, new()
    {
        private readonly ICRUDRepository<T> _repository;

        public RelationsCrudService(ICRUDRepository<T> repository)
        {
            _repository = repository;
        }

        public void Save(T entity)
        {
            _repository.Save(entity);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }





    }
}
