using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
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

namespace SIMS_Booking.Utility.Timers
{
    internal class NotificationTimer
    {
        private DateTime _date;
        private DispatcherTimer _checkDateTimer;

        private readonly ReservationService _reservationService;
        private readonly GuestReviewService _guestReviewService;
        private readonly PostponementService _postponementService;
        private readonly ForumService _forumService;
        private readonly User _user;
        public ObservableCollection<Reservation> ReservedAccommodations { get; set; }


        public NotificationTimer(User user, PostponementService postponementService = null, ObservableCollection<Reservation> reservedAccommodations = null,
                                 ReservationService reservationService = null, GuestReviewService guestReviewService = null, ForumService forumService = null)
        {
            ReservedAccommodations = reservedAccommodations;
            _reservationService = reservationService;
            _guestReviewService = guestReviewService;
            _postponementService = postponementService;
            _forumService = forumService;
            _user = user;

            if (_user.Role == Enums.Roles.Owner)
            {
                ReviewNotifications();
                CancellationNotifications();
                ForumNotification();
            }
            else if (_user.Role == Enums.Roles.Guest1)
                OwnerReviewedNotification();
        }

        private void ForumNotification()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                List<Forum> forums = _forumService.ShouldNotifyOwner(_user.GetId());

                if (forums.Any())
                {
                    notifier.ShowInformation("New forum");
                    foreach (Forum forum in forums)
                    {
                        forum.OwnersToNotify[_user.GetId()] = true;
                        _forumService.Update(forum);
                    }
                }

                timer.Stop();
            };
            timer.Start();
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
                    _postponementService.SetNotifiedPostponements();
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
                foreach (Reservation reservation in _reservationService.GetAll().ToList())
                {
                    if (reservation.IsCanceled && !reservation.IsCancellationReviewed)
                    {
                        notifier.ShowInformation("Your reservation has been canceled");
                        reservation.IsCancellationReviewed = true;
                        _reservationService.Update(reservation);
                    }
                    
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

                _reservationService.RemoveUnratedReservations(_guestReviewService);
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

                _reservationService.RemoveUnratedReservations(_guestReviewService);
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
