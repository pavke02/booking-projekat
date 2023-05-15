using SIMS_Booking.Model;
using System.Collections.Generic;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class RidesService
    {
        private readonly ICRUDRepository<Rides> _repository;

        public RidesService(ICRUDRepository<Rides> repository)
        {
            _repository = repository;
        }

        #region Crud 

        public void Save(Rides ride)
        {
            _repository.Save(ride);
        }

        public List<Rides> GetAll()
        {
            return _repository.GetAll();
        }

        public Rides GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Delete(Rides ride)
        {
            _repository.Delete(ride);
        }

        #endregion





    }
}
