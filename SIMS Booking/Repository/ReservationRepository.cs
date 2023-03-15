using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMS_Booking.Repository
{
    public class ReservationRepository : Repository<Reservation>, ISubject
    {
        public ReservationRepository() : base("../../../Resources/Data/reservations.csv") { }        

        public ObservableCollection<Reservation> GetReservationsByUser(int userId)
        {
            ObservableCollection<Reservation> userReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in _entityList)
            {
                 if (reservation.User.getID() == userId)
                 {
                    userReservations.Add(reservation);
                 }
            }
           
           return userReservations;            
        }

        public List<Reservation> GetUnreviewedReservations()
        {
            return _entityList.Where(e => !e.IsReviewed).ToList();                
        }
    }
}
