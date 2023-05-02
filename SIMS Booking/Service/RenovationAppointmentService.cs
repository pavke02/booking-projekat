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

        public void Save(RenovationAppointment renovationAppointment)
        {
            _crudService.Save(renovationAppointment);
        }
    }
}
