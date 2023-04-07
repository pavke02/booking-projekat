using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class GuestReviewService
    {
        private readonly GuestReviewRepository _repository;

        public GuestReviewService()
        {
            _repository = new GuestReviewRepository();
        }

        public void Save(GuestReview guestReview)
        {
            _repository.Save(guestReview);
        }

        public void LoadReservationInGuestReview(ReservationService _reservationService)
        {
            foreach (GuestReview guestReview in _repository.GetAll())
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
            return _repository.GetAll().Where(e => e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
        }        

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
