using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class TourReviewService
    {
        private readonly ICRUDRepository<TourReview> _repository;

        public TourReviewService(ICRUDRepository<TourReview> repository)
        {
            _repository = repository;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public List<TourReview> GetAll()
        {
            return _repository.GetAll();
        }

        public void loadusers(UserService userservice,ConfirmTourService confirmTourService)
        {
            foreach (var tour in confirmTourService.GetAll())
            {
                tour.User = userservice.GetById(tour.UserId);
            }
        }
        public void loadCheckPoints(ConfirmTourService confirmTourService, TourReviewService tourReviewService)
        {
            foreach (var tour in tourReviewService.GetAll())
            {
                tour.ConfirmTour = confirmTourService.GetById(tour.ConfirmTourId);
            }
        }


        public List<TourReview> recenzije(ConfirmTourService confirmTourService, Tour tour)
        {
            List<TourReview> reviewsOfTour = new List<TourReview>();

            foreach (ConfirmTour confirmTour in confirmTourService.GetAll())
            {
                foreach (TourReview tourReview in _repository.GetAll())
                {
                    if (tourReview.ConfirmTourId == confirmTour.Id)
                    {
                        reviewsOfTour.Add(tourReview);
                    }
                }
            }

            return reviewsOfTour;
        }

        public void updateIsValidReview(TourReview tourReview)
        {
            tourReview.IsValid = true;
            _repository.Update(tourReview);
        }

    }

}
