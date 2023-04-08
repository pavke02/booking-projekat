using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedAccommodationService
    {
        private readonly ReservedAccommodationCsvCrudRepository _csvCrudRepository;

        public ReservedAccommodationService()
        {
            _csvCrudRepository = new ReservedAccommodationCsvCrudRepository();
        }

        public void Save(ReservedAccommodation reservedAccommodation)
        {
            _csvCrudRepository.Save(reservedAccommodation);
        }        

        public void LoadAccommodationsAndUsersInReservation(UserService userService, AccommodationService accommodationService, ReservationService reservationService)
        {
            foreach (ReservedAccommodation reservedAccommodation in _csvCrudRepository.GetAll())
            {
                foreach (Reservation reservation in reservationService.GetAll())
                {
                    if (reservedAccommodation.ReservationId == reservation.getID())
                    {
                        reservation.Accommodation = accommodationService.GetById(reservedAccommodation.AccommodationId);
                        reservation.User = userService.GetById(reservedAccommodation.UserId);
                    }
                }
            }
        }
    }
}
