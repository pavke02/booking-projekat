using SIMS_Booking.Model;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository
{
    public class GuestReviewCsvCrudRepository : CsvCrudRepository<GuestReview>, ISubject
    {
        public GuestReviewCsvCrudRepository() : base("../../../Resources/Data/guestReviews.csv") { }                 
    }
}
