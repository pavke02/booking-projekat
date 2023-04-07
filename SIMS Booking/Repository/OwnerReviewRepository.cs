using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    class OwnerReviewRepository: Repository<OwnerReview>, ISubject
    {
        public OwnerReviewRepository() : base("../../../Resources/Data/ownerReviews.csv") { }
    }
}
