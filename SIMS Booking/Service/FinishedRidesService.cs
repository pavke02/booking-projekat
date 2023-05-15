using SIMS_Booking.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class FinishedRidesService
    {
        private readonly ICRUDRepository<FinishedRide> _repository;

        public FinishedRidesService(ICRUDRepository<FinishedRide> repository)
        {
            _repository = repository;
        }

        #region Crud 

        public void Save(FinishedRide finishedRide)
        {
            _repository.Save(finishedRide);
        }

        public List<FinishedRide> GetAll()
        {
            return _repository.GetAll();
        }

        public FinishedRide GetById(int id)
        {
            return _repository.GetById(id);
        }

        #endregion
    }
}
