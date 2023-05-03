using SIMS_Booking.Model;
using SIMS_Booking.Service;
using System;
using System.Linq;
using System.Windows.Threading;

namespace SIMS_Booking.Utility.Timers
{
    public class DatePassedTimer
    {
        private readonly AccommodationService _accommodationService;
        private readonly RenovationAppointmentService _renovationAppointmentService;
        private readonly User _user;
        private DispatcherTimer _checkDateTimer;

        public DatePassedTimer(AccommodationService accommodationService, RenovationAppointmentService renovationAppointmentService, User user)
        {
            _accommodationService = accommodationService;
            _renovationAppointmentService = renovationAppointmentService;
            _user = user;

            if (_user.Role == Enums.Roles.Owner || _user.Role == Enums.Roles.Guest1)
            {
                RenovatingAppointmentPassed();
                YearPassed();
            }
        }

        private void YearPassed()
        {
            CheckYear(null, null);

            _checkDateTimer = new DispatcherTimer();
            _checkDateTimer.Tick += new EventHandler(CheckYear);
            _checkDateTimer.Interval = new TimeSpan(0, 1, 0);
            _checkDateTimer.Start();
        }

        private void RenovatingAppointmentPassed()
        {
            CheckRenovatingAppointments(null, null);

            _checkDateTimer = new DispatcherTimer();
            _checkDateTimer.Tick += new EventHandler(CheckRenovatingAppointments);
            _checkDateTimer.Interval = new TimeSpan(0, 1, 0);
            _checkDateTimer.Start();
        }

        private void CheckRenovatingAppointments(object sender, EventArgs e)
        {
            foreach (RenovationAppointment renovationAppointment in _renovationAppointmentService.GetAll().ToList())
            {
                if (renovationAppointment.EndDate <= DateTime.Now)
                {
                    renovationAppointment.IsRenovating = false;
                    renovationAppointment.Accommodation.IsRenovated = true;

                    _renovationAppointmentService.Update(renovationAppointment);
                    _accommodationService.Update(renovationAppointment.Accommodation);
                }
            }
        }

        private void CheckYear(object sender, EventArgs e)
        {
            foreach (RenovationAppointment renovationAppointment in _renovationAppointmentService.GetAll().ToList())
            {
                if ((renovationAppointment.EndDate - DateTime.Now).Days < 0 && Math.Abs((renovationAppointment.EndDate - DateTime.Now).Days) >= 365)
                {
                    renovationAppointment.Accommodation.IsRenovated = false;
                    _accommodationService.Update(renovationAppointment.Accommodation);
                }
            }
        }
    }
}
