using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;

namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedAccommodationService
    {
        private readonly RelationsCrudService<ReservedAccommodation> _crudService;

        public ReservedAccommodationService()
        {
            _crudService = new RelationsCrudService<ReservedAccommodation>("../../../Resources/Data/reservedAccommodations.csv");
        }

        public void Save(ReservedAccommodation reservedAccommodation)
        {
            _crudService.Save(reservedAccommodation);
        }        

        public void LoadAccommodationsAndUsersInReservation(UserService userService, AccommodationService accommodationService, ReservationService reservationService)
        {
            foreach (ReservedAccommodation reservedAccommodation in _crudService.GetAll())
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
