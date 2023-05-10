using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class OwnerReviewService
    {
        private readonly CrudService<OwnerReview> _crudService;

        public OwnerReviewService()
        {
            _crudService = new CrudService<OwnerReview>("../../../Resources/Data/ownerReviews.csv");
        }

        #region Crud

        public void Save(OwnerReview ownerReview)
        {
            _crudService.Save(ownerReview);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public List<OwnerReview> GetByUserId(int id)
        {
            return _crudService.GetAll().Where(e => e.Reservation.User.getID() == id).ToList();
        }

        public void LoadReservationInOwnerReview(ReservationService _reservationService)
        {
            foreach (OwnerReview ownerReview in _crudService.GetAll())
            {
                ownerReview.Reservation = _reservationService.GetById(ownerReview.ReservationId);
            }
        }

        public void SubmitReview(int tidiness, int ownerCorrectness, string comment, Reservation reservation, List<string> images, bool hasRenovation, int renovationLevel, string renovationComment)
        {
            reservation.HasGuestReviewed = true;
            OwnerReview ownerReview = new OwnerReview(tidiness, ownerCorrectness, comment, reservation, images, hasRenovation, renovationLevel, renovationComment);
            Save(ownerReview);
        }

        public List<OwnerReview> GetShowableReviews(int id)
        {
            return _crudService.GetAll().Where(e => e.Reservation.HasGuestReviewed && e.Reservation.HasOwnerReviewed && e.Reservation.Accommodation.User.getID() == id).ToList();
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

        public Dictionary<string, int> GetRenovationsByYear(int id)
        {
            Dictionary<string, int> ownerRatings = new Dictionary<string, int>();

            foreach (OwnerReview ownerRating in _crudService.GetAll().Where(e => e.Reservation.Accommodation.getID() == id && e.HasRenovation))
            {
                string key = ownerRating.Reservation.StartDate.Year.ToString();
                if (ownerRatings.ContainsKey(key))
                    ownerRatings[key] += 1;
                else
                    ownerRatings[key] = 1;
            }

            return ownerRatings;
        }

        public Dictionary<int, int> GetRenovationsByMonth(int id, int year)
        {
            Dictionary<int, int> ownerRatings = new Dictionary<int, int>();

            foreach (OwnerReview ownerRating in _crudService.GetAll().Where(e => e.Reservation.Accommodation.getID() == id && 
                         e.HasRenovation && e.Reservation.StartDate.Year == year))
            {
                int key = ownerRating.Reservation.StartDate.Month;
                if (ownerRatings.ContainsKey(key))
                    ownerRatings[key] += 1;
                else
                    ownerRatings[key] = 1;
            }

            return ownerRatings;
        }
    }
}
