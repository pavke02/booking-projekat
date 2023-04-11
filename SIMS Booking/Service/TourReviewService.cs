using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class TourReviewService
    {
        private readonly TourReviewRepository _repository;

        public TourReviewService()
        {
            _repository = new TourReviewRepository();
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public List<TourReview> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(TourReview tour)
        {
            _repository.Save(tour);
        }

        public TourReview GetById(int id)
        {
            return _repository.GetById(id);
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
