using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using SIMS_Booking.Model;
using SIMS_Booking.UI.ViewModel.Owner;
using SIMS_Booking.Utility.Observer;

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

        public void Subsctibe(IObserver observer)
        {
            _crudService.Subscribe(observer);
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
            return GetAll().Where(e => e.Accommodation.User.getID() == id && e.IsRenovating).ToList();
        }

        public List<RenovationAppointment> GetPastRenovations(int id)
        {
            return GetAll().Where(e => e.Accommodation.User.getID() == id && !e.IsRenovating).ToList();
        }
    }
}
