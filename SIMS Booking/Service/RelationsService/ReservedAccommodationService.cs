using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedAccommodationService
    {
        private readonly ICRUDRepository<ReservedAccommodation> _repository;

        public ReservedAccommodationService(ICRUDRepository<ReservedAccommodation> repository)
        {
            _repository = repository;
        }

        public void Save(ReservedAccommodation reservedAccommodation)
        {
            _repository.Save(reservedAccommodation);
        }        

        public void LoadAccommodationsAndUsersInReservation(UserService userService, AccommodationService accommodationService, ReservationService reservationService)
        {
            foreach (ReservedAccommodation reservedAccommodation in _repository.GetAll())
            {
                foreach (Reservation reservation in reservationService.GetAll())
                {
                    if (reservedAccommodation.ReservationId == reservation.GetId())
                    {
                        reservation.Accommodation = accommodationService.GetById(reservedAccommodation.AccommodationId);
                        reservation.User = userService.GetById(reservedAccommodation.UserId);
                    }
                }
            }
        }
    }
}
