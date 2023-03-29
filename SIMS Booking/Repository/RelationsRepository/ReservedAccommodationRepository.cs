using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;

namespace SIMS_Booking.Repository.RelationsRepository
{
    public class ReservedAccommodationRepository : RelationsRepository<ReservedAccommodation>
    {
        public ReservedAccommodationRepository() : base("../../../Resources/Data/reservedAccommodations.csv") { }

        public void LoadAccommodationsAndUsersInReservation(UserRepository userRepository, AccommodationRepository accomodationRepository, ReservationRepository reservationRepository)
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

        public void DeleteByReservation(int reservationId)
        {
            foreach (ReservedAccommodation reservedAccommodation in _entityList)
                if(reservedAccommodation.ReservationId == reservationId)
                {
                    Delete(reservedAccommodation);
                    break;
                }                    
        }       
    }
}
