using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                 if (reservation.User.ID == userId)
                 {
                    userReservations.Add(reservation);
                 }
            }
           
           return userReservations;            
        }
    }
}
