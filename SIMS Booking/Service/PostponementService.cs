using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Service
{
    public class PostponementService
    {
        private readonly PostponementRepository _repository;

        public PostponementService()
        {
            _repository = new PostponementRepository();
        }

        public List<Postponement> Load()
        {
            return _repository.Load();
        }

        public void Save(Postponement postponement)
        {
            _repository.Save(postponement);
        }

        public List<Postponement> GetAll()
        {
            return _repository.GetAll();
        }

        public Postponement GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);
        }

        public void LoadReservationInPostponement(ReservationService reservationService)
        {
            foreach (Postponement postponement in GetAll())
            {
                postponement.Reservation = reservationService.GetById(postponement.ReservationId);
            }
        }

        public ObservableCollection<Postponement> GetPostponementsByUser(int userId)
        {
            ObservableCollection<Postponement> userReservations = new ObservableCollection<Postponement>();
            foreach (Postponement postponement in _repository.GetAll())
            {

                if (postponement.Reservation.User.getID() == userId)
                    userReservations.Add(postponement);
            }

            return userReservations;
        }

        public void DeletePostponementsByReservationId(int reservationId)
        {
            foreach (Postponement postponement in _repository.GetAll().ToList())
            {
                if (postponement.ReservationId == reservationId)
                {
                    _repository.Delete(postponement);
                }
            }
        }
    }
}
