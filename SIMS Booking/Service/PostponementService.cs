using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SIMS_Booking.Enums;

namespace SIMS_Booking.Service
{
    public class PostponementService
    {
        private readonly CrudService<Postponement> _crudService;

        public PostponementService()
        {
            _crudService = new CrudService<Postponement>("../../../Resources/Data/postponements.csv");
        }

        #region Crud

        public void Save(Postponement postponement)
        {
            _crudService.Save(postponement);
        }

        public List<Postponement> GetAll()
        {
            return _crudService.GetAll();
        }

        public Postponement GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        #endregion

        public List<Postponement> GetByUserId(int id)
        {
            return GetAll().Where(e => e.Reservation.Accommodation.User.getID() == id && e.Status == Enums.PostponementStatus.Pending).ToList();
        }

        public void ReviewPostponementRequest(int id, string comment, PostponementStatus status)
        {
            Postponement postponement = GetById(id);
            postponement.Status = status;
            postponement.Comment = comment;
            _crudService.Update(postponement);
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
            foreach (Postponement postponement in GetAll())
            {

                if (postponement.Reservation.User.getID() == userId)
                    userReservations.Add(postponement);
            }

            return userReservations;
        }

        public void DeletePostponementsByReservationId(int reservationId)
        {
            foreach (Postponement postponement in GetAll().ToList())
            {
                if (postponement.ReservationId == reservationId)
                {
                    _crudService.Delete(postponement);
                }
            }
        }

        public List<Postponement> GetReviewedPostponements()
        {
            List<Postponement> postponements = new List<Postponement>();

            foreach (Postponement postponement in GetAll())
            {
                if (postponement.Status != Enums.PostponementStatus.Pending && !postponement.IsNotified)
                {
                    postponements.Add(postponement);
                }
            }

            return postponements;
        }

        public void SetNotifiedPostpoments()
        {
            foreach (Postponement postponement in GetAll().ToList())
            {
                if (postponement.Status != Enums.PostponementStatus.Pending)
                {
                    postponement.IsNotified = true;
                    _crudService.Update(postponement);
                }
            }
        }
    }
}
