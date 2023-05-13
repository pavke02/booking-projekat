using SIMS_Booking.Model;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class TourRequestService
    {
        private readonly ICRUDRepository<TourRequest> _repository;

        public TourRequestService(ICRUDRepository<TourRequest> repository)
        {
            _repository = repository;
        }
    }
}
