using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class ReservationService
    {
        private readonly ICRUDRepository<Reservation> _repository;

        public ReservationService(ICRUDRepository<Reservation> repository)
        {
            _repository = repository;
        }

        #region Crud

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
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

        #endregion

        public List<Reservation> GetByAccommodation(int id) 
        {
            return _repository.GetAll().Where(e => e.Accommodation.getID() == id && e.EndDate >= DateTime.Now).ToList();
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

        public void PostponeReservation(int reservationId, DateTime newStartDate, DateTime newEndDate)
        {
            Reservation reservation = GetById(reservationId);
            reservation.StartDate = newStartDate;
            reservation.EndDate = newEndDate;
            _repository.Update(reservation);
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

        public bool isSuperGuest(User user)
        {
            int reservationCounter = 0;
            foreach (Reservation reservation in _repository.GetAll())
            {
                if (reservation.User == user && DateTime.Today - reservation.EndDate < TimeSpan.FromDays(365))
                    reservationCounter++;
            }

            if (reservationCounter >= 5)
                return true;
            return false;

        }

        public Dictionary<string, int> GetReservationsByYear(int id)
        {
            Dictionary<string, int> reservations = new Dictionary<string, int>();

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.getID() == id))
            {
                string key = reservation.StartDate.Year.ToString();
                if (reservations.ContainsKey(key))
                    reservations[key] += 1;
                else
                    reservations[key] = 1;
            }

            return reservations;
        }

        public Dictionary<int, int> GetReservationsByMonth(int id, int year)
        {
            Dictionary<int, int> reservations = new Dictionary<int, int>();

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.getID() == id && e.StartDate.Year == year))
            {
                int key = reservation.StartDate.Month;
                if (reservations.ContainsKey(key))
                    reservations[key] += 1;
                else
                    reservations[key] = 1;
            }

            return reservations;
        }
    }
}
