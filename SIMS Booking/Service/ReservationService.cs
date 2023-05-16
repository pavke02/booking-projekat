using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public List<Reservation> GetActiveByAccommodation(int id) 
        {
            return GetActiveReservations().Where(e => e.Accommodation.GetId() == id && e.EndDate >= DateTime.Now).ToList();
        }

        public ObservableCollection<Reservation> GetReservationsByUser(int userId)
        {
            ObservableCollection<Reservation> userReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in _repository.GetAll())
            {
                if (reservation.User.GetId() == userId && !reservation.IsCanceled)
                    userReservations.Add(reservation);
            }                

            return userReservations;
        }

        public List<Reservation> GetActiveReservations()
        {
            return _repository.GetAll().Where(e => !e.IsCanceled).ToList();
        }

        public List<Reservation> GetUnreviewedActiveReservations(int id)
        {
            return GetActiveReservations().Where(e => !e.HasOwnerReviewed && e.Accommodation.User.GetId() == id).ToList();
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

            foreach (Reservation reservation in GetActiveReservations())
            {

                if (reservation.Accommodation.GetId() == selectedAccommodation.GetId())
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

        public bool IsSuperGuest(User user)
        {
            int reservationCounter = 0;
            foreach (Reservation reservation in GetReservationsByUser(user.GetId()))
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

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.GetId() == id))
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

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.GetId() == id && e.StartDate.Year == year))
            {
                int key = reservation.StartDate.Month;
                if (reservations.ContainsKey(key))
                    reservations[key] += 1;
                else
                    reservations[key] = 1;
            }

            return reservations;
        }

        public Dictionary<string, int> GetCancellationsByYear(int id)
        {
            Dictionary<string, int> reservations = new Dictionary<string, int>();

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.GetId() == id && e.IsCanceled))
            {
                string key = reservation.StartDate.Year.ToString();
                if (reservations.ContainsKey(key))
                    reservations[key] += 1;
                else
                    reservations[key] = 1;
            }

            return reservations;
        }

        public Dictionary<int, int> GetCancellationsByMonth(int id, int year)
        {
            Dictionary<int, int> reservations = new Dictionary<int, int>();

            foreach (Reservation reservation in GetAll().Where(e => e.Accommodation.GetId() == id && e.StartDate.Year == year && e.IsCanceled))
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
