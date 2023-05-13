using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class GuideReviewService
    {
        private readonly ICRUDRepository<GuideReview> _repository;

        public GuideReviewService(ICRUDRepository<GuideReview> repository)
        {
            _repository = repository;
        }

        #region Crud

        public void Save(GuideReview guideReview)
        {
            _repository.Save(guideReview);
        }
        
        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        #endregion

        public List<GuideReview> GetByUserId(int id)
        {
            return _repository.GetAll().Where(e => e.TourReservation.UserId == id).ToList();
        }
        /*
        public void LoadReservationInOwnerReview(ReservedTourService _reservedTourService)
        {
           foreach (GuideReview guideReview in _repository.GetAll())
            {
                guideReview.TourReservation = _reservedTourService.GetById(guideReview.getID());
            }
           
        }
        */
        public void SubmitReview(int tourRating, TourReservation tourReservation, List<string> images)
        {
            //tourReservation = new TourReservation();
            
            tourReservation.HasGuestReviewed = true;
            GuideReview guideReview = new GuideReview(tourRating, tourReservation, images);
            Save(guideReview);
        }
        
        
        public List<GuideReview> GetShowableReviews(int id)
        {
            return _repository.GetAll().Where(e => e.TourReservation.HasGuestReviewed && e.TourReservation.HasGuideReviewed && e.TourReservation.TourId == id).ToList();
        }
        
        public double CalculateRating(int id)
        {
            int numberOfGuestReviews = GetShowableReviews(id).Count();
            if (numberOfGuestReviews == 0)
                return 0;
            double ratingSum = 0;
            foreach (GuideReview guideReview in GetShowableReviews(id))
            {
                ratingSum += (double)guideReview.TourRating;
            }

            return ratingSum / numberOfGuestReviews;
        }
        
    }
}
