using System;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.UI.ViewModel.Owner;

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

        public void Update(Accommodation accommodation)
        {
            _crudService.Update(accommodation);
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

        public List<string> GetAccommodationNames(int id)
        {
            List<string> names = new List<string>();
            foreach (Accommodation accommodation in GetAll())
            {
                names.Add(accommodation.Name);
            }

            return names;
        }
    }
}
