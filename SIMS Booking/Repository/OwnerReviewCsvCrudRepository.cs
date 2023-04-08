using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    class OwnerReviewCsvCrudRepository: CsvCrudRepository<OwnerReview>, ISubject
    {
        public OwnerReviewCsvCrudRepository() : base("../../../Resources/Data/ownerReviews.csv") { }
    }
}
