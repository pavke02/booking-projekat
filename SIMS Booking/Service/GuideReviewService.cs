using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.Utility.Observer;
using System.Collections.Generic;
using System.Linq;


namespace SIMS_Booking.Service
{
    public class GuideReviewService
    {
        private readonly CrudService<GuideReview> _crudService;

        public GuideReviewService()
        {
            _crudService = new CrudService<GuideReview>("../../../Resources/Data/guideReviews.csv");
        }

        #region Crud

        public void Save(GuideReview guideReview)
        {
            _crudService.Save(guideReview);
        }
        
        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public List<GuideReview> GetByUserId(int id)
        {
            return _crudService.GetAll().Where(e => e.TourReservation.UserId == id).ToList();
        }
        /*
        public void LoadReservationInOwnerReview(ReservedTourService _reservedTourService)
        {
           foreach (GuideReview guideReview in _crudService.GetAll())
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
            return _crudService.GetAll().Where(e => e.TourReservation.HasGuestReviewed && e.TourReservation.HasGuideReviewed && e.TourReservation.TourId == id).ToList();
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
