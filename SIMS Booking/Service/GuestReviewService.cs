using System.Collections;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class GuestReviewService
    {
        private readonly ICRUDRepository<GuestReview> _repository;

        public GuestReviewService(ICRUDRepository<GuestReview> repository)
        {
            _repository = repository;
        }

        #region Crud
        public List<GuestReview> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(GuestReview guestReview)
        {
            _repository.Save(guestReview);
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
        #endregion

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
            return _repository.GetAll().Where(e => e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.GetId() == id).ToList();
        }
    }
}
