using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace SIMS_Booking.Service
{
    public class ReservationService
    {
        private readonly CrudService<Reservation> _crudService;

        public ReservationService()
        {
            _crudService = new CrudService<Reservation>("../../../Resources/Data/reservations.csv");
        }

        #region Crud

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public void Save(Reservation reservation)
        {
            _crudService.Save(reservation);
        }

        public void Update(Reservation reservation)
        {
            _crudService.Update(reservation);
        }

        public List<Reservation> GetAll()
        {
            return _crudService.GetAll();
        }

        public Reservation GetById(int id)
        {
            return _crudService.GetById(id);
        }

        #endregion

        public List<Reservation> GetByAccommodation(int id) 
        {
            return _crudService.GetAll().Where(e => e.Accommodation.getID() == id).ToList();
        }

        public ObservableCollection<Reservation> GetReservationsByUser(int userId)
        {
            ObservableCollection<Reservation> userReservations = new ObservableCollection<Reservation>();
            foreach (Reservation reservation in _crudService.GetAll())
            {
                
                if (reservation.User.getID() == userId)
                    userReservations.Add(reservation);
            }                

            return userReservations;
        }

        public List<Reservation> GetUnreviewedReservations(int id)
        {
            return _crudService.GetAll().Where(e => !e.HasOwnerReviewed && e.Accommodation.User.getID() == id).ToList();
        }

        //Metoda proverava da li je istekao rok za ocenjivanje,
        //i ako jeste izbacuje rezervaciju iz liste rezervisanih smestaja i stavlja je u istoriju rezervacija(neocenjenu)
        public void RemoveUnreviewedReservations(GuestReviewService guestReviewService)
        {
            foreach (Reservation reservation in _crudService.GetAll().ToList())
                if ((DateTime.Now - reservation.EndDate).TotalDays > 5 && !reservation.HasOwnerReviewed)
                {
                    reservation.HasOwnerReviewed = true;
                    _crudService.Update(reservation);
                    GuestReview guestReview = new GuestReview(0, 0, null, reservation);
                    guestReviewService.Save(guestReview);
                }
        }

        public List<Reservation> GetAccommodationReservations(Accommodation selectedAccommodation)
        {
            List<Reservation> accommodationReservations = new List<Reservation>();

            foreach (Reservation reservation in _crudService.GetAll())
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
            _crudService.Update(reservation);
        }

        public void DeleteCancelledReservation(int id)
        {
            foreach (Reservation reservation in _crudService.GetAll().ToList())
            {
                if (reservation.getID() == id)
                {
                    _crudService.Delete(reservation);
                }
            }
        }
    }
}
