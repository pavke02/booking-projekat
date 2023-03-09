using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using System.Linq;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ReservedAccommodationRepository : RelationsRepository<ReservedAccommodation>
    {
        public ReservedAccommodationRepository() : base("../../../Resources/Data/reservedAccommodations.csv") { }

        public void LoadAccommodationsAndUsersInReservation(UserRepository userRepository, AccomodationRepository accomodationRepository, ReservationRepository reservationRepository)
        {
            foreach(ReservedAccommodation reservedAccommodation in _entityList)
            {                
                foreach (Reservation reservation in reservationRepository.GetAll())
                {
                    if (reservedAccommodation.ReservationId == reservation.getID())
                    {
                        reservation.Accommodation = accomodationRepository.GetById(reservedAccommodation.AccommodationId);
                        reservation.User = userRepository.GetById(reservedAccommodation.UserId);
                    }
                }
            }
        }
    }
}
