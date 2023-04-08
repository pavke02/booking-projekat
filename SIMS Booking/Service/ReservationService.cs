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
        private readonly ReservationCsvCrudRepository _csvCrudRepository;

        public ReservationService()
        {
            _csvCrudRepository = new ReservationCsvCrudRepository();
        }

        public void Save(Reservation reservation)
        {
            _csvCrudRepository.Save(reservation);  
        }

        public void Update(Reservation reservation)
        {
            _csvCrudRepository.Update(reservation);
        }

        public List<Reservation> GetAll()
        {
            return _csvCrudRepository.GetAll();
        }

        public Reservation GetById(int id)
        {
            return _csvCrudRepository.GetById(id);
        }     
        
        public List<Reservation> GetByAccommodation(int id) 
        {
            return _csvCrudRepository.GetAll().Where(e => e.Accommodation.getID() == id).ToList();
        }

        public ObservableCollection<Reservation> GetReservationsByUser(int userId)
        {
            ObservableCollection<Reservation> userReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in _csvCrudRepository.GetAll())
            {
                
                if (reservation.User.getID() == userId)
                    userReservations.Add(reservation);
            }                

            return userReservations;
        }

        public List<Reservation> GetUnreviewedReservations(int id)
        {
            return _csvCrudRepository.GetAll().Where(e => !e.HasOwnerReviewed && e.Accommodation.User.getID() == id).ToList();
        }

        //Metoda proverava da li je istekao rok za ocenjivanje,
        //i ako jeste izbacuje rezervaciju iz liste rezervisanih smestaja i stavlja je u istoriju rezervacija(neocenjenu)
        public void RemoveUnreviewedReservations(GuestReviewService guestReviewService)
        {
            foreach (Reservation reservation in _csvCrudRepository.GetAll().ToList())
                if ((DateTime.Now - reservation.EndDate).TotalDays > 5 && !reservation.HasOwnerReviewed)
                {
                    reservation.HasOwnerReviewed = true;
                    _csvCrudRepository.Update(reservation);
                    GuestReview guestReview = new GuestReview(0, 0, null, reservation);
                    guestReviewService.Save(guestReview);
                }
        }

        public List<Reservation> GetAccommodationReservations(Accommodation selectedAccommodation)
        {
            List<Reservation> accommodationReservations = new List<Reservation>();

            foreach (Reservation reservation in _csvCrudRepository.GetAll())
            {

                if (reservation.Accommodation.getID() == selectedAccommodation.getID())
                {
                    accommodationReservations.Add(reservation);
                }
            }

            return accommodationReservations;
        }

        public void PostponeReservation(int reservationId, DateTime newStartDate, DateTime newEndDate)
        {
            Reservation reservation = GetById(reservationId);
            reservation.StartDate = newStartDate;
            reservation.EndDate = newEndDate;
            _csvCrudRepository.Update(reservation);
        }

        public void Subscribe(IObserver observer)
        {
            _csvCrudRepository.Subscribe(observer);
        }

        public void DeleteCancelledReservation(int id)
        {
            foreach (Reservation reservation in _csvCrudRepository.GetAll().ToList())
            {
                if (reservation.getID() == id)
                {
                    _csvCrudRepository.Delete(reservation);
                }
            }
        }
    }
}
