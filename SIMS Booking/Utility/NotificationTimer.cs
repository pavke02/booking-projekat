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
using Microsoft.TeamFoundation.Common;
using SIMS_Booking.Service;

namespace SIMS_Booking.Utility
{    
    internal class NotificationTimer
    {
        private DateTime _date;
        private DispatcherTimer _checkDateTimer;

        private readonly ReservationService _reservationService;
        private readonly GuestReviewService _guestReviewService;
        private readonly PostponementService _postponementService;
        private readonly CancellationCsvCrudRepository _cancellationCsvCrudRepository;
        private readonly User _user;
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }


        public NotificationTimer(User user, PostponementService postponementService = null, ObservableCollection<Reservation> reservedAccommodations = null, 
                                 ReservationService reservationService = null, GuestReviewService guestReviewService = null, 
                                 CancellationCsvCrudRepository cancellationCsvCrudRepository = null)
        {
            ReservedAccommodations = reservedAccommodations;
            _reservationService = reservationService;
            _guestReviewService = guestReviewService;
            _postponementService = postponementService;
            _cancellationCsvCrudRepository = cancellationCsvCrudRepository;
            _user = user;

            if (_user.Role == Enums.Roles.Owner)
            {
                ReviewNotifications();
                CancellationNotifications();
            }
            else if (_user.Role == Enums.Roles.Guest1)
                OwnerReviewedNotification();
        }


        ~NotificationTimer()
        {
            if (_checkDateTimer != null)
                _checkDateTimer.Stop();

            notifier.Dispose();
        }

        private void OwnerReviewedNotification()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                if (!_postponementService.GetReviewedPostponements().IsNullOrEmpty())
                {
                    notifier.ShowInformation("Owner has reviewed your postponement requests");
                    _postponementService.SetNotifiedPostpoments();
                }

                timer.Stop();
            };
            timer.Start();
        }

        public void CancellationNotifications()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                if (!_cancellationCsvCrudRepository.GetAll().IsNullOrEmpty())
                    notifier.ShowInformation("Your reservation has been canceled");
                foreach (Reservation reservation in _cancellationCsvCrudRepository.GetAll().ToList())
                {
                    _cancellationCsvCrudRepository.Delete(reservation);
                }

                timer.Stop();
            };
            timer.Start();
        }


        public void ReviewNotifications()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                if (ReservedAccommodations.FirstOrDefault(s => s.EndDate <= DateTime.Now && (DateTime.Now - s.EndDate.Date).TotalDays <= 5) != null)
                    notifier.ShowInformation("You have guests to review!");

                _reservationService.RemoveUnreviewedReservations(_guestReviewService);
                timer.Stop();
            };
            timer.Start();

            _date = DateTime.Now;
            _checkDateTimer = new DispatcherTimer();
            _checkDateTimer.Tick += new EventHandler(CheckDate);
            _checkDateTimer.Interval = new TimeSpan(0, 1, 0);
            _checkDateTimer.Start();
        }

        //Metoda koja proverava da li user idalje moze da se oceni naspram datuma. 
        //Metoda se poziva na svakih 1min za slucaj da se datum promeni u tom periodu
        public void CheckDate(object sender, EventArgs e)
        {
            if (_date.Date != DateTime.Now.Date)
            {
                _date = DateTime.Now;
                if (ReservedAccommodations.FirstOrDefault(s => s.EndDate <= DateTime.Now && (DateTime.Now - s.EndDate.Date).TotalDays <= 5) != null)
                    notifier.ShowInformation("You have guests to review!");

                _reservationService.RemoveUnreviewedReservations(_guestReviewService);
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
