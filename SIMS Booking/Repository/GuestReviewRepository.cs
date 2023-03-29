using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class GuestReviewRepository : Repository<GuestReview>, ISubject
    {
        public GuestReviewRepository() : base("../../../Resources/Data/guestReviews.csv") { }                 
    }
}
