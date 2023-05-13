using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class RenovationAppointmentService
    {
        private readonly CrudService<RenovationAppointment> _crudService;

        public RenovationAppointmentService()
        {
            _crudService = new CrudService<RenovationAppointment>("../../../Resources/Data/renovationAppointment.csv");
        }

        #region Crud
        public void Save(RenovationAppointment renovationAppointment)
        {
            _crudService.Save(renovationAppointment);
        }

        public List<RenovationAppointment> GetAll()
        {
            return _crudService.GetAll();
        }

        public void Update(RenovationAppointment renovationAppointment)
        {
            _crudService.Update(renovationAppointment);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public void Delete(RenovationAppointment renovationAppointment)
        {
            _crudService.Delete(renovationAppointment);
        }

        public RenovationAppointment GetById(int id)
        {
            return _crudService.GetById(id);
        }
        #endregion

        public void LoadAccommodationInRenovationAppointment(AccommodationService accommodationService)
        {
            foreach (RenovationAppointment renovationAppointment in _crudService.GetAll())
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
    }
}
