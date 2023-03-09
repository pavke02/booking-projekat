using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class GuestReviewRepository : Repository<GuestReview>, ISubject
    {
        public GuestReviewRepository() : base("../../../Resources/Data/guestReviews.csv") { }
    }
}
