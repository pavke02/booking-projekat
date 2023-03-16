using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class GuestReviewRepository : Repository<GuestReview>, ISubject
    {
        public GuestReviewRepository() : base("../../../Resources/Data/guestReviews.csv") { }     
        
        public void LoadReservationInGuestReview(ReservationRepository _reservationRepository)
        {
            foreach(GuestReview guestReview in _entityList)
            {
                guestReview.Reservation = _reservationRepository.GetById(guestReview.ReservationId);
            }
        }

        public void SubmitReview(int tidiness, int ruleFollowing, string comment, Reservation reservation)
        {
            reservation.IsReviewed = true;
            GuestReview guestReview = new GuestReview(tidiness, ruleFollowing, comment, reservation);
            Save(guestReview);
        }

        public List<GuestReview> GetReviewedReservations()
        {
            return _entityList.Where(e => e.Reservation.IsReviewed).ToList();
        }
    }
}
