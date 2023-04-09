using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class GuestReviewService
    {
        private readonly CrudService<GuestReview> _crudService;

        public GuestReviewService()
        {
            _crudService = new CrudService<GuestReview>("../../../Resources/Data/guestReviews.csv");
        }

        #region Crud

        public void Save(GuestReview guestReview)
        {
            _crudService.Save(guestReview);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public void LoadReservationInGuestReview(ReservationService _reservationService)
        {
            foreach (GuestReview guestReview in _crudService.GetAll())
            {
                guestReview.Reservation = _reservationService.GetById(guestReview.ReservationId);
            }
        }

        public void SubmitReview(int tidiness, int ruleFollowing, string comment, Reservation reservation)
        {
            reservation.HasOwnerReviewed = true;
            GuestReview guestReview = new GuestReview(tidiness, ruleFollowing, comment, reservation);
            Save(guestReview);
        }

        public List<GuestReview> GetReviewedReservations(int id)
        {
            return _crudService.GetAll().Where(e => e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
        }
    }
}
