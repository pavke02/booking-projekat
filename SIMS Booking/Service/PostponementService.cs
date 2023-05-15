using SIMS_Booking.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class PostponementService
    {
        private readonly ICRUDRepository<Postponement> _repository;

        public PostponementService(ICRUDRepository<Postponement> repository)
        {
            _repository = repository;
        }

        #region Crud
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

        #endregion

        public List<Postponement> GetByUserId(int id)
        {
            return GetAll().Where(e => e.Reservation.Accommodation.User.GetId() == id && e.Status == Enums.PostponementStatus.Pending).ToList();
        }

        public void ReviewPostponementRequest(int id, string comment, PostponementStatus status)
        {
            Postponement postponement = GetById(id);
            postponement.Status = status;
            postponement.Comment = comment;
            _repository.Update(postponement);
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
                if (postponement.Reservation.User.GetId() == userId)
                    userReservations.Add(postponement);
            }

            return userReservations;
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

        public void SetNotifiedPostponements()
        {
            foreach (Postponement postponement in GetAll().ToList())
            {
                if (postponement.Status != Enums.PostponementStatus.Pending)
                {
                    postponement.IsNotified = true;
                    _repository.Update(postponement);
                }
            }
        }

        public List<Postponement> GetByAccommodation(int id)
        {
            return GetAll().Where(e => e.Reservation.Accommodation.GetId() == id).ToList();
        }

        public Dictionary<string, int> GetPostponementsByYear(int id)
        {
            Dictionary<string, int> postponements = new Dictionary<string, int>();

            foreach (Postponement postponement in GetByAccommodation(id))
            {
                string key = postponement.Reservation.StartDate.Year.ToString();
                if (postponements.ContainsKey(key))
                    postponements[key] += 1;
                else
                    postponements[key] = 1;
            }

            return postponements;
        }

        public Dictionary<int, int> GetPostponementsByMonth(int id, int year)
        {
            Dictionary<int, int> postponements = new Dictionary<int, int>();

            foreach (Postponement postponement in GetByAccommodation(id).Where(e => e.Reservation.StartDate.Year == year))
            {
                int key = postponement.Reservation.StartDate.Month;
                if (postponements.ContainsKey(key))
                    postponements[key] += 1;
                else
                    postponements[key] = 1;
            }

            return postponements;
        }
    }
}
