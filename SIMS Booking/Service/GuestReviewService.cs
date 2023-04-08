using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class GuestReviewService
    {
        private readonly GuestReviewCsvCrudRepository _csvCrudRepository;

        public GuestReviewService()
        {
            _csvCrudRepository = new GuestReviewCsvCrudRepository();
        }

        public void Save(GuestReview guestReview)
        {
            _csvCrudRepository.Save(guestReview);
        }

        public void LoadReservationInGuestReview(ReservationService _reservationService)
        {
            foreach (GuestReview guestReview in _csvCrudRepository.GetAll())
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
            return _csvCrudRepository.GetAll().Where(e => e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
        }        

        public void Subscribe(IObserver observer)
        {
            _csvCrudRepository.Subscribe(observer);
        }
    }
}
