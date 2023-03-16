using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System;
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

        //Metoda proverava da li je istekao rok za ocenjivanje,
        //i ako jeste izbacuje rezervaciju iz liste rezervisanih smestaja i stavlja je u istoriju rezervacija(neocenjenu)
        public void RemoveUnreviewedReservations(GuestReviewRepository guestReviewRepository)
        {
            foreach(Reservation reservation in _entityList.ToList()) 
                if((DateTime.Now - reservation.EndDate).TotalDays > 5 && !reservation.IsReviewed)
                {
                    reservation.IsReviewed = true;
                    Update(reservation);
                    GuestReview guestReview = new GuestReview(0, 0, null, reservation);
                    guestReviewRepository.Save(guestReview);
                }                    
        }
    }
}
