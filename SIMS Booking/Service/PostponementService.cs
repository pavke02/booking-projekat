﻿using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.TestManagement.WebApi;

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

        public List<Postponement> GetByUserId(int id)
        {
            return _repository.GetAll().Where(e => e.Reservation.Accommodation.User.getID() == id && e.Status == Enums.PostponementStatus.Pending).ToList();
        }

        public void ApprovePostponement(int id)
        {
            Postponement postponement = GetById(id);
            postponement.Status = Enums.PostponementStatus.Accepted;
            postponement.Comment = "Approved";
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

        /*public void Subscribe(IObserver observer)
        {
            return _repository.GetAll().Where(e => e.Reservation.Accommodation.User.getID() == id && e.Status == Enums.PostponementStatus.Pending).ToList();
        }*/

        public List<Postponement> GetReviewedPostponements()
        {
            List<Postponement> postponements = new List<Postponement>();

            foreach (Postponement postponement in _repository.GetAll())
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
            List<Postponement> postponements = _repository.GetAll();

            foreach (Postponement postponement in _repository.GetAll().ToList())
            {
                if (postponement.Status != Enums.PostponementStatus.Pending)
                {
                    postponement.IsNotified = true;
                    _repository.Update(postponement);
                }
            }


        }

    }
}
