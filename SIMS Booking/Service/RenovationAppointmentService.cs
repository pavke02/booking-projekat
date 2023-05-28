using System.Collections;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class RenovationAppointmentService
    {
        private readonly ICRUDRepository<RenovationAppointment> _repository;

        public RenovationAppointmentService(ICRUDRepository<RenovationAppointment> repository)
        {
            _repository = repository;
        }

        #region Crud
        public void Save(RenovationAppointment renovationAppointment)
        {
            _repository.Save(renovationAppointment);
        }

        public List<RenovationAppointment> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(RenovationAppointment renovationAppointment)
        {
            _repository.Update(renovationAppointment);
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void Delete(RenovationAppointment renovationAppointment)
        {
            _repository.Delete(renovationAppointment);
        }

        public RenovationAppointment GetById(int id)
        {
            return _repository.GetById(id);
        }
        #endregion

        public void LoadAccommodationInRenovationAppointment(AccommodationService accommodationService)
        {
            foreach (RenovationAppointment renovationAppointment in _repository.GetAll())
            {
                renovationAppointment.Accommodation =
                    accommodationService.GetById(renovationAppointment.AccommodationId);
            }
        }

        public List<RenovationAppointment> GetActiveRenovations(int id)
        {
            return GetAll().Where(e => e.Accommodation.User.GetId() == id && e.IsRenovating).ToList();
        }

        public List<RenovationAppointment> GetPastRenovations(int id)
        {
            return GetAll().Where(e => e.Accommodation.User.GetId() == id && !e.IsRenovating).ToList();
        }

        public List<RenovationAppointment> GetActiveByAccommodation(int id)
        {
            return GetAll().Where(e => e.Accommodation.GetId() == id).ToList();
        }
    }
}
