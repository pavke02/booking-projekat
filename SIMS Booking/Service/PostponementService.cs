using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
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
    }
}
