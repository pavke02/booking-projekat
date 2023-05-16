using SIMS_Booking.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public  class GroupRideService
    {
        private readonly ICRUDRepository<GroupRide> _repository;

        public GroupRideService(ICRUDRepository<GroupRide> repository)
        {
            _repository = repository;
        }

        #region Crud 

        public void Save(GroupRide groupRide)
        {
            _repository.Save(groupRide);
        }

        public List<GroupRide> GetAll()
        {
            return _repository.GetAll();
        }

        public GroupRide GetById(int id)
        {
            return _repository.GetById(id);
        }

        #endregion




    }
}
