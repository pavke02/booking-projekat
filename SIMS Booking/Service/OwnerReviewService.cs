using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class OwnerReviewService
    {
        private readonly OwnerReviewRepository _repository;

        public OwnerReviewService()
        {
            _repository = new OwnerReviewRepository();
        }

        public List<OwnerReview> GetByUserId(int id)
        {
            return _repository.GetAll().Where(e => e.Reservation.User.getID() == id).ToList();
        }

        public void LoadReservationInOwnerReview(ReservationService _reservationService)
        {
            foreach (OwnerReview ownerReview in _repository.GetAll())
            {
                ownerReview.Reservation = _reservationService.GetById(ownerReview.ReservationId);
            }
        }

        //public void SubmitReview(int tidiness, int ruleFollowing, string comment, Reservation reservation)
        //{
        //    reservation.IsReviewed = true;
        //    GuestReview guestReview = new GuestReview(tidiness, ruleFollowing, comment, reservation);
        //    Save(guestReview);
        //}

        public List<OwnerReview> GetShowableReviews(int id)
        {
            return _repository.GetAll().Where(e => e.Reservation.HasGuestReviewed && e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
        }

        public double CalculateRating(int id)
        {
            int numberOfGuestReviews = GetShowableReviews(id).Count();
            if (numberOfGuestReviews == 0)
                return 0;
            double ratingSum = 0;
            foreach (OwnerReview ownerReview in GetShowableReviews(id))
            {
                ratingSum += (double)ownerReview.Tidiness + (double)ownerReview.OwnersCorrectness;
            }

            return ratingSum / numberOfGuestReviews;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
