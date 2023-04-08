using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class AccommodationService
    {
        private readonly AccommodationCsvCrudRepository _csvCrudRepository;

        public AccommodationService()
        {
            _csvCrudRepository = new AccommodationCsvCrudRepository();
        }

        public void Save(Accommodation accommodation)
        {
            _csvCrudRepository.Save(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return _csvCrudRepository.GetAll();
        }

        public Accommodation GetById(int id) 
        {
            return _csvCrudRepository.GetById(id);
        }        

        public List<Accommodation> GetByUserId(int id)
        {
            return _csvCrudRepository.GetAll().Where(e => e.User.getID() == id).ToList();
        }

        public List<Accommodation> SortBySuperOwner(List<Accommodation> accommodations)
        {
            return accommodations.OrderBy(x => !x.User.IsSuperUser).ToList();
        }

        public void Subscribe(IObserver observer)
        {
            _csvCrudRepository.Subscribe(observer);
        }
    }
}
