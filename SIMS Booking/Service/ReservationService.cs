using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;

        public ReservationService()
        {
            _repository = new ReservationRepository();
        }

        public void Save(Reservation reservation)
        {
            _repository.Save(reservation);  
        }

        public void Update(Reservation reservation)
        {
            _repository.Update(reservation);
        }

        public List<Reservation> GetAll()
        {
            return _repository.GetAll();
        }

        public Reservation GetById(int id)
        {
            return _repository.GetById(id);
        }        

        public ObservableCollection<Reservation> GetReservationsByUser(int userId)
        {
            ObservableCollection<Reservation> userReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in _repository.GetAll())
            {
                
                if (reservation.User.getID() == userId)
                    userReservations.Add(reservation);
            }                

            return userReservations;
        }

        public void RemoveReservation(Reservation reservationToDelete)
        {

            List<Reservation> reservations = _repository.GetAll();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.getID() == reservationToDelete.getID())
                {
                    _repository.Delete(reservation);
                }
            }
        }

        public List<Reservation> GetUnreviewedReservations(int id)
        {
            return _repository.GetAll().Where(e => !e.HasOwnerReviewed && e.Accommodation.User.getID() == id).ToList();
        }

        //Metoda proverava da li je istekao rok za ocenjivanje,
        //i ako jeste izbacuje rezervaciju iz liste rezervisanih smestaja i stavlja je u istoriju rezervacija(neocenjenu)
        public void RemoveUnreviewedReservations(GuestReviewService guestReviewService)
        {
            foreach (Reservation reservation in _repository.GetAll().ToList())
                if ((DateTime.Now - reservation.EndDate).TotalDays > 5 && !reservation.HasOwnerReviewed)
                {
                    reservation.HasOwnerReviewed = true;
                    _repository.Update(reservation);
                    GuestReview guestReview = new GuestReview(0, 0, null, reservation);
                    guestReviewService.Save(guestReview);
                }
        }

        public List<Reservation> GetAccommodationReservations(Accommodation selectedAccommodation)
        {
            List<Reservation> accommodationReservations = new List<Reservation>();

            foreach (Reservation reservation in _repository.GetAll())
            {

                if (reservation.Accommodation.getID() == selectedAccommodation.getID())
                {
                    accommodationReservations.Add(reservation);
                }
            }

            return accommodationReservations;
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void DeleteCancelledReservation(int id)
        {
            foreach (Reservation reservation in _repository.GetAll().ToList())
            {
                if (reservation.getID() == id)
                {
                    _repository.Delete(reservation);
                }
            }
        }
    }
}
