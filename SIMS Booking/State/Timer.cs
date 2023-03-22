using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using System.Windows;

namespace SIMS_Booking.State
{    
    internal class Timer
    {
        private DateTime _date;
        private DispatcherTimer _checkDateTimer;

        private ReservationRepository _reservationRepository;
        private GuestReviewRepository _guestReviewRepository;
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }

        public Timer(ObservableCollection<Reservation> reservedAccommodations, ReservationRepository reservationRepository, GuestReviewRepository guestReviewRepository)
        {
            ReservedAccommodations = reservedAccommodations;
            _reservationRepository = reservationRepository;
            _guestReviewRepository = guestReviewRepository;

            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                if (ReservedAccommodations.FirstOrDefault(s => s.EndDate <= DateTime.Now && (DateTime.Now - s.EndDate.Date).TotalDays <= 5) != null)
                    notifier.ShowInformation("You have guests to review!");

                _reservationRepository.RemoveUnreviewedReservations(_guestReviewRepository);
                timer.Stop();
            };
            timer.Start();

            _date = DateTime.Now;
            _checkDateTimer = new DispatcherTimer();
            _checkDateTimer.Tick += new EventHandler(CheckDate);
            _checkDateTimer.Interval = new TimeSpan(0, 1, 0);
            _checkDateTimer.Start();
        }

        ~Timer() { _checkDateTimer.Stop(); notifier.Dispose(); }

        //Metoda koja proverava da li user idalje moze da se oceni naspram datuma. 
        //Metoda se poziva na svakih 1min za slucaj da se datum promeni u tom periodu
        public void CheckDate(object sender, EventArgs e)
        {
            if (_date.Date != DateTime.Now.Date)
            {
                _date = DateTime.Now;
                if (ReservedAccommodations.FirstOrDefault(s => s.EndDate <= DateTime.Now && (DateTime.Now - s.EndDate.Date).TotalDays <= 5) != null)
                    notifier.ShowInformation("You have guests to review!");

                _reservationRepository.RemoveUnreviewedReservations(_guestReviewRepository);
            }
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(1));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
