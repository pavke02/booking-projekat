using System.Collections.Generic;
using System.Windows.Documents;
using SIMS_Booking.Model;

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
        #endregion

        public void LoadAccommodationInRenovationAppointment(AccommodationService accommodationService)
        {
            foreach (RenovationAppointment renovationAppointment in _crudService.GetAll())
            {
                renovationAppointment.Accommodation =
                    accommodationService.GetById(renovationAppointment.AccommodationId);
            }
        }
    }
}
