using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Repository.RelationsRepository;
using System.Collections.Generic;

namespace SIMS_Booking.Service.RelationsService
{
    public class ReservedAccommodationService
    {
        private readonly ReservedAccommodationRepository _repository;

        public ReservedAccommodationService()
        {
            _repository = new ReservedAccommodationRepository();
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
