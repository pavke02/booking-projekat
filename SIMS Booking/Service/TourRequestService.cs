using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Markup;
using SIMS_Booking.Model;
using SIMS_Booking.Repository;


namespace SIMS_Booking.Service
{
    public class TourRequestService
    {
        private readonly ICRUDRepository<TourRequest> _repository;

        public TourRequestService(ICRUDRepository<TourRequest> repository)
        {
            _repository = repository;
        }

        public List<TourRequest> GetRequestTours()
        {
            List<TourRequest> RequestTours = new List<TourRequest>();

            foreach (var tours in _repository.GetAll())
            {
                if(tours.Requests != Enums.Requests.Accepted)
                     RequestTours.Add(tours);
                
            }
            return RequestTours;
        }

       
       


        public List<TourRequest> GetToursByNumberOfGuests(int numberOfGuests)
        {
            List<TourRequest> RequestToursByNumberOfGuests = new List<TourRequest>();

            foreach (var tours in _repository.GetAll())
            {
                if (numberOfGuests == tours.NumberOfGuests)
                {
                    if (tours.Requests != Enums.Requests.Accepted)
                        RequestToursByNumberOfGuests.Add(tours);
                }
            }
            return RequestToursByNumberOfGuests;
        }

        public List<TourRequest> GetRequestToursByDate(DateTime starts, DateTime ends)
        {
            List<TourRequest> RequestToursByDate = new List<TourRequest>();

            foreach (var tours in _repository.GetAll())
            {
                if (tours.TimeOfStart >= starts && tours.TimeOfEnd <= ends)
                {
                    if (tours.Requests != Enums.Requests.Accepted)
                            RequestToursByDate.Add(tours);
                }
            }
            return RequestToursByDate;
        }

        public List<TourRequest> GetRequestToursByLocation(string country, string city)
        {
            List<TourRequest> RequestToursByLocation = new List<TourRequest>();

            foreach (var tours in _repository.GetAll())
            {
                if (tours.Location.Country == country && tours.Location.City == city)
                {
                    if (tours.Requests != Enums.Requests.Accepted)
                        RequestToursByLocation.Add(tours);
                }
            }
            return RequestToursByLocation;
        }

        public List<TourRequest> GetToursByLanguage(string language)
        {
            List<TourRequest> RequestToursByLanguage = new List<TourRequest>();

            foreach (var tours in _repository.GetAll())
            {
                if (tours.Language == language)
                {
                    RequestToursByLanguage.Add(tours);
                }
            }
            return RequestToursByLanguage;
        }

        public int NumberOfRequestsByLanguage(string language, int year)
        {
            int counter = 0;
            foreach (var tours in _repository.GetAll())
            {
                if (tours.Language == language && (tours.TimeOfStart.Year == year || tours.TimeOfEnd.Year == year))
                {
                    counter++;
                }
            }
            return counter;
        }

        public int NumberOfRequestsByLocation(string country, string city, int year)
        {
            int counter = 0;
            foreach (var tours in _repository.GetAll())
            {
                if (tours.Location.Country == country && tours.Location.City == city && (tours.TimeOfStart.Year == year || tours.TimeOfEnd.Year == year))
                {
                    counter++;
                }
            }
            return counter;
        }


        public int NumberOfRequestsByLanguageInMonth(string language, int year, int month)
        {
            int counter = 0;
            foreach (var tours in _repository.GetAll())
            {
                if (tours.Language == language && (tours.TimeOfStart.Year == year || tours.TimeOfEnd.Year == year) && (tours.TimeOfStart.Month == month || tours.TimeOfEnd.Month == month))
                {
                    counter++;
                }
            }
            return counter;
        }

        public Dictionary<int, int> NumberOfRequestsBylanguageInMonth1(string language, int year)
        {
            Dictionary<int, int> brojPonavljanjaPoMesecima = new Dictionary<int, int>();

            
            foreach (var tura in _repository.GetAll())
            {
                if (tura.Language == language && tura.TimeOfStart.Year == year)
                {
                    int mesec = tura.TimeOfStart.Month;

                    if (brojPonavljanjaPoMesecima.ContainsKey(mesec))
                    {
                        brojPonavljanjaPoMesecima[mesec]++;
                    }
                    else
                    {
                        brojPonavljanjaPoMesecima[mesec] = 1;
                    }

                }
               
            }

                return brojPonavljanjaPoMesecima;

        }

        public Dictionary<int, int> GetAllTimeRequestToursStats(string language) 
        {
           
            Dictionary<int,int> stats = new Dictionary<int, int>();
            List<int> years = _repository.GetAll().Where(x => x.Language == language).Select(x => x.TimeOfStart.Year).Distinct().ToList();
            
            foreach (var year in years)
            {               

                int TourCount = _repository.GetAll().Where(x =>  x.Language == language && x.TimeOfStart.Year == year).Count();
                
                stats.Add(year, TourCount);
            }

            return stats;
        }

        public Dictionary<string, int> GetRequestToursStatsByYear(int year,string language)
        {
            Dictionary<string, int> monthlyStats;
            Dictionary<string,int> stats = new Dictionary<string, int>();
            for (int i = 1; i < 13; i++)
            {
                

               int MonthCounter = _repository.GetAll().Where(x => x.Language == language && x.TimeOfStart.Month == i && x.TimeOfStart.Year == year).Count();
               
                stats[DateTimeFormatInfo.CurrentInfo.GetMonthName(i)] = MonthCounter;
            }
            return stats;
        }


        public string MostCommonLanguage()
        {
            Dictionary<string, int> jezikBrojac = new Dictionary<string, int>();

            foreach (var podaci in _repository.GetAll())
            {
                string jezik = podaci.Language;

                if (jezikBrojac.ContainsKey(jezik))
                {
                    jezikBrojac[jezik]++;
                }
                else
                {
                    jezikBrojac[jezik] = 1;
                }
            }

            string najcesciJezik = jezikBrojac.OrderByDescending(x => x.Value).FirstOrDefault().Key;

            return najcesciJezik;

        }


        public Dictionary<string,string> MostCommonLocation()
        {
            Dictionary<string, string> lokacija = new Dictionary<string, string>();

            Dictionary<string, int> gradBrojac = new Dictionary<string, int>();

            foreach (var podaci in _repository.GetAll())
            {
                if(podaci.DateOfSendRequest.AddYears(-1) <= DateTime.Now)
                {

                    string grad = podaci.Location.City;

                    if (gradBrojac.ContainsKey(grad))
                    {
                        gradBrojac[grad]++;
                    }
                    else
                    {
                        gradBrojac[grad] = 1;
                    }


                }
               
            }

            string najcesciGrad = gradBrojac.OrderByDescending(x => x.Value).FirstOrDefault().Key;

            string country;
            foreach(var tour in _repository.GetAll())
            {
                if(tour.Location.City ==  najcesciGrad)
                {
                    country = tour.Location.Country;
                    lokacija.Add(country, najcesciGrad);
                    break;
                    
                }
            }
            
            return lokacija;

        }

        public void Update(TourRequest tourRequest)
        {
            _repository.Update(tourRequest);
        }

    }

    



       
}
