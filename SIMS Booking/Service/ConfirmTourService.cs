﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.Service
{
    public class ConfirmTourService
    {

        private readonly ConfirmTourRepository _repository;
        public int godina { get; set; }
        public TimeOnly vreme { get; set; }
        public ConfirmTour ConfirmTour { get; set; }



        public ConfirmTourService()
        {
            _repository = new ConfirmTourRepository();

        }

        public void Subscribe(IObserver observer)
        {
            _repository.Subscribe(observer);

        }


        public List<ConfirmTour> GetAll()
        {
            return _repository.GetAll();
        }

        public void Save(ConfirmTour tour)
        {
            _repository.Save(tour);
        }

        public ConfirmTour? Update(ConfirmTour entity)
        {

            return _repository.Update(entity);
        }

        public void Delete(ConfirmTour entity)
        {
            _repository.Delete(entity);

        }


        public void loadGuests(UserService userService)
        {
            foreach (ConfirmTour tour in _repository.GetAll())
            {
                tour.User = userService.GetById(tour.UserId);
            }
        }
        public ConfirmTour GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<User> GetGuestOnTour(Tour selectedTour)
        {
            List<User> GuestOnTour = new List<User>();

            foreach (ConfirmTour tour in _repository.GetAll())
            {
                if (tour.IdTour == selectedTour.getID())
                {
                    if (tour.IdCheckpoint < 0)
                        GuestOnTour.Add(tour.User);


                }
                else
                    continue;
            }
            return GuestOnTour;


        }

        public int GetNumberOfGuestOnTour(Tour selectedTour)
        {
            int brojac = 0;
            foreach (ConfirmTour confirmTour in _repository.GetAll())
            {
                if (confirmTour.IdTour == selectedTour.getID() && confirmTour.IdCheckpoint != -5)
                {
                    brojac++;
                }
            }

            return brojac;
        }

        public K FindFirstKeyByValue<K, V>(Dictionary<K, V> dict, V val)
        {
            return dict.FirstOrDefault(entry =>
                EqualityComparer<V>.Default.Equals(entry.Value, val)).Key;
        }

        public Tour MostVisitedTour(TourService tourService)
        {
            Tour MaxTura;
            int numberOfVisitors = 0;
            Dictionary<Tour, int> NumberOfGuestOnTour = new Dictionary<Tour, int>();
           
            foreach (Tour tour in tourService.GetAll())
            {
                foreach (ConfirmTour confirmTour in _repository.GetAll())
                {

                    if (confirmTour.IdTour == tour.getID() && confirmTour.IdCheckpoint >= 0)
                    {
                        numberOfVisitors++;

                    }
                }
                NumberOfGuestOnTour.Add(tour, numberOfVisitors);
                numberOfVisitors = 0;

            }

            MaxTura = FindFirstKeyByValue(NumberOfGuestOnTour, NumberOfGuestOnTour.Values.Max());
            return MaxTura;


        }

        public Tour MostVisitedTourByYear(TourService tourService, int godina)
        {
            Tour MaxTura;
            int numberOfVisitors = 0;
            Dictionary<Tour, int> NumberOfGuestOnTour = new Dictionary<Tour, int>();

            foreach (Tour tour in tourService.GetAll())
            {
                foreach (ConfirmTour confirmTour in _repository.GetAll())
                {

                    if (confirmTour.IdTour == tour.getID() && confirmTour.IdCheckpoint >= 0 && tour.StartTour.Year == godina)
                    {
                        numberOfVisitors++;

                    }
                }
                NumberOfGuestOnTour.Add(tour, numberOfVisitors);
                numberOfVisitors = 0;

            }

            MaxTura = FindFirstKeyByValue(NumberOfGuestOnTour, NumberOfGuestOnTour.Values.Max());
            return MaxTura;


        }

        public bool AddVaucer(Tour tour, TimeOnly vreme, ConfirmTour confirmTour)
        {
            if (tour.StartTour > DateTime.Now.AddHours(48))
            {
                var lista = _repository.GetAll();
                for (int i = 0;i <lista.Count ; i++)
                {
                    if (tour.getID() == lista[i].IdTour)
                    {
                        lista[i].Vaucer += 1;
                        lista[i].IdTour = -1;
                        _repository.Update(lista[i]);
                    }
                }

                return true;

            }
            return false;
        }

        
        
        public int NumberOfGuesteByAgesUnder18(UserService userService, Tour tour)
        {
            int under18 = 0;
            foreach (ConfirmTour confirmTour in _repository.GetAll())
            {
                if (confirmTour.IdTour == tour.getID())
                {
                    foreach (User users in userService.GetAll())
                    {
                        if (confirmTour.UserId == users.getID())
                        {
                            if (users.Age <18)
                                under18++;
                        }
                    }
                }
            }
            return under18;
        }

     


        public int NumberOfGuestsByAgesBetween18and50(UserService userService,Tour tour)
        {
            int between18and50 = 0;
            
            foreach (ConfirmTour confirmTour in _repository.GetAll())
            {
                if (confirmTour.IdTour == tour.getID())
                {
                    foreach (User users in userService.GetAll())
                    {
                        if (confirmTour.UserId == users.getID())
                        {
                            if (users.Age >= 18 && users.Age <= 50)
                                between18and50++;
                        }
                    }
                }
            }


            Trace.WriteLine(between18and50);
            return between18and50;
        }

        public int NumberOfGuestByAgesUp50(UserService userService,Tour tour)
        {
            int up50 = 0;
            foreach (ConfirmTour confirmTour in _repository.GetAll())
            {
                if (confirmTour.IdTour == tour.getID())
                {
                    foreach (User users in userService.GetAll())
                    {
                        if (confirmTour.UserId == users.getID())
                        {
                            if (users.Age > 50)
                                up50++;
                        }
                    }
                }
            }
            return up50;
        }


        public String PercentageByVaucer(Tour tour)
        {
            string nijeGotova = "Tura nije gotova";
            if (tour.StartTour < DateTime.Today)
            {
                float saVaucerom = 0;
                float brojLjudi = 0;
                foreach (ConfirmTour confirmTour in _repository.GetAll())
                {
                    if (confirmTour.IdTour == tour.getID())
                    {
                        brojLjudi++;

                        if (confirmTour.Vaucer > 0)
                        {
                            saVaucerom++;
                        }

                    }
                }
                Trace.WriteLine(string.Format("{0:P}", saVaucerom / brojLjudi));
                return string.Format("{0:P}", saVaucerom / brojLjudi);
            }
            else
                return nijeGotova;
        }

        public String PercentageWithoutVaucer(Tour tour)
        {
            string nijeGotova = "Tura nije gotova";
            if(tour.StartTour < DateTime.Today)
            {
            float saVaucerom = 0;
            float brojLjudi = 0;
            foreach (ConfirmTour confirmTour in _repository.GetAll())
            {
                if (confirmTour.IdTour == tour.getID())
                {
                    brojLjudi++;

                    if (confirmTour.Vaucer > 0)
                     saVaucerom++;
    
                }
            }
            Trace.WriteLine(string.Format("{0:P}",1- saVaucerom / brojLjudi));
            return string.Format("{0:P}",1- saVaucerom / brojLjudi);

            }
            else
                return nijeGotova;

        }

        //public List<int> allCheckPoints(TourReviewService tourReviewService)
        //{
        //    List<int> listCP = new List<int>();
        //    foreach(ConfirmTour confirmTour in _repository.GetAll())
        //    {
        //        foreach(TourReview tourReview in tourReviewService.GetAll())
        //        {
        //            if(confirmTour.IdTour == tourReview.TourId && confirmTour.UserId == tourReview.UserId)
        //            {
        //                listCP.Add(confirmTour.IdCheckpoint);
        //            }
        //        }
        //    }
        //    return listCP;

        //}

        



    }
}
