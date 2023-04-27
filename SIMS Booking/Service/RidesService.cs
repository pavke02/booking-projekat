using SIMS_Booking.Model;
using System.Collections.Generic;

namespace SIMS_Booking.Service
{
    public class RidesService
    {
        private readonly CrudService<Rides> _crudService;

        public RidesService()
        {
            _crudService = new CrudService<Rides>("../../../Resources/Data/rides.csv");
        }

        #region Crud 

        public void Save(Rides ride)
        {
            _crudService.Save(ride);
        }

        public List<Rides> GetAll()
        {
            return _crudService.GetAll();
        }

        public Rides GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Delete(Rides ride)
        {
            _crudService.Delete(ride);
        }

        #endregion





    }
}
