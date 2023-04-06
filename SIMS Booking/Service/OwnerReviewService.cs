﻿using SIMS_Booking.Model;
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

        public void Save(OwnerReview ownerReview)
        {
            _repository.Save(ownerReview);
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

        public void SubmitReview(int tidiness, int ownerCorrectness, string comment, Reservation reservation)
        {
            reservation.HasGuestReviewed = true;
            OwnerReview ownerReview = new OwnerReview(tidiness, ownerCorrectness, comment, reservation);
            Save(ownerReview);
        }

        public List<OwnerReview> GetShowableReviews(int id)
        {
            return _repository.GetAll().Where(e => e.Reservation.HasGuestReviewed && e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }
    }
}
