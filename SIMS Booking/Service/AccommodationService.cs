using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class AccommodationService
    {
        private readonly ICRUDRepository<Accommodation> _repository;

        public AccommodationService(ICRUDRepository<Accommodation> repository)
        {
            _repository = repository;
        }

        #region Crud
        public void Save(Accommodation accommodation)
        {
            _repository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return _repository.GetAll();
        }

        public Accommodation GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void Update(Accommodation accommodation)
        {
            _repository.Update(accommodation);
        }
        #endregion

        public List<Accommodation> GetByUserId(int id)
        {
            return _repository.GetAll().Where(e => e.User.GetId() == id).ToList();
        }

        public List<Accommodation> SortBySuperOwner(List<Accommodation> accommodations)
        {
            return accommodations.OrderBy(x => !x.User.IsSuperUser).ToList();
        }

        public List<Accommodation> GetAccommodationsByLocation(Location unpopularLocation)
        {
            return GetAll().Where(e => e.Location.City == unpopularLocation.City && e.Location.Country == unpopularLocation.Country).ToList();
        }

        public bool OwnerHasAccommodationOnLocation(Location selectedForumLocation, int userId)
        {
            return GetAll().FirstOrDefault(x => x.User.GetId() == userId && 
                                                x.Location.City == selectedForumLocation.City && 
                                                x.Location.Country == selectedForumLocation.Country) != null;
        }
    }
}
