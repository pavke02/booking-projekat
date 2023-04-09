using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class AccommodationService
    {
        private readonly CrudService<Accommodation> _crudService;

        public AccommodationService()
        {
            _crudService = new CrudService<Accommodation>("../../../Resources/Data/accommodations.csv");
        }

        #region Crud

        public void Save(Accommodation accommodation)
        {
            _crudService.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return _crudService.GetAll();
        }

        public Accommodation GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public List<Accommodation> GetByUserId(int id)
        {
            return _crudService.GetAll().Where(e => e.User.getID() == id).ToList();
        }

        public List<Accommodation> SortBySuperOwner(List<Accommodation> accommodations)
        {
            return accommodations.OrderBy(x => !x.User.IsSuperUser).ToList();
        }
    }
}
