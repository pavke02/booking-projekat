
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class AccommodationService
    {
        private readonly AccommodationRepository _repository;

        public AccommodationService()
        {
            _repository = new AccommodationRepository();
        }

        public List<Accommodation> Load()
        {
            return _repository.Load();
        }

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

        public List<Accommodation> GetByUserId(int id)
        {
            return _repository.GetAll().Where(e => e.User.getID() == id).ToList();
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
