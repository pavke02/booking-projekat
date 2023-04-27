using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SIMS_Booking.Model;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.Service
{
    public class ConfirmTourService
    {
        private readonly CrudService<ConfirmTour> _crudService;

        public ConfirmTourService()
        {
            _crudService = new CrudService<ConfirmTour>("../../../Resources/Data/confirmTours.csv");
        }

        public void Subscribe(IObserver observer)
        {
            _crudService.Subscribe(observer);
        }

        public List<ConfirmTour> GetAll()
        {
            return _crudService.GetAll();
        }

        public ConfirmTour? Update(ConfirmTour entity)
        {

            return _crudService.Update(entity);
        }

        public void Delete(ConfirmTour entity)
        {
            _crudService.Delete(entity);

        }

        public void loadGuests(UserService userService)
        {
            foreach (ConfirmTour tour in _crudService.GetAll())
            {
                tour.User = userService.GetById(tour.UserId);
            }
        }
        public ConfirmTour GetById(int id)
        {
            return _crudService.GetById(id);
        }

        public List<User> GetGuestOnTour(Tour selectedTour)
        {
            List<User> GuestOnTour = new List<User>();

            foreach (ConfirmTour tour in _crudService.GetAll())
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
            foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
                foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
                foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
                var lista = _crudService.GetAll();
                for (int i = 0;i <lista.Count ; i++)
                {
                    if (tour.getID() == lista[i].IdTour)
                    {
                        lista[i].Vaucer += 1;
                        lista[i].IdTour = -1;
                        _crudService.Update(lista[i]);
                    }
                }

                return true;

            }
            return false;
        }

        public int NumberOfGuesteByAgesUnder18(UserService userService, Tour tour)
        {
            int under18 = 0;    
            foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
            
            foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
            foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
                foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
            foreach (ConfirmTour confirmTour in _crudService.GetAll())
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
    }
}
