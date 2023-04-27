using System.Collections.Generic;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class TourReviewService
    {
        private readonly CrudService<TourReview> _crudService;

        public TourReviewService()
        {
            _crudService = new CrudService<TourReview>("../../../Resources/Data/tourReview.csv");
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public List<TourReview> GetAll()
        {
            return _crudService.GetAll();
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
                foreach (TourReview tourReview in _crudService.GetAll())
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
            _crudService.Update(tourReview);
        }

    }

}
