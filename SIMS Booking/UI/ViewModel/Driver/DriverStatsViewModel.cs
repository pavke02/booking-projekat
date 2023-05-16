using SIMS_Booking.UI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Stores;
using System.Windows.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Service.NavigationService;

namespace SIMS_Booking.UI.ViewModel.Driver
{
    public class DriverStatsViewModel : ViewModelBase
    {
        public List<MonthlyDriverStats> MonthlyDriverStats2023 { get; set; }
        public List<MonthlyDriverStats> MonthlyDriverStats2022 { get; set; }
        public List<MonthlyDriverStats> MonthlyDriverStats2021 { get; set; }

        public User User;

        private FinishedRidesService _finishedRidesService;

        public List<FinishedRide> FinishedRides { get; set; }

        public ICommand NavigateBackCommand { get; }


        public DriverStatsViewModel(FinishedRidesService finishedRidesService, User user, ModalNavigationStore modalNavigationStore)
        {

            User = user;

            MonthlyDriverStats2023 = new List<MonthlyDriverStats>();
            MonthlyDriverStats2022 = new List<MonthlyDriverStats>();
            MonthlyDriverStats2021 = new List<MonthlyDriverStats>();

            _finishedRidesService = finishedRidesService;

            GenerateEmptyList(MonthlyDriverStats2021);
            GenerateEmptyList(MonthlyDriverStats2022);
            GenerateEmptyList(MonthlyDriverStats2023);

            FinishedRides = new List<FinishedRide>();
            int id = User.GetId();
            FinishedRides = _finishedRidesService.GetAll().Where(e => e.Ride.DriverID == id).ToList();

            SortStats(MonthlyDriverStats2023, MonthlyDriverStats2022, MonthlyDriverStats2021);

            NavigateBackCommand = new NavigateBackCommand(CreateCloseStatsNavigationService(modalNavigationStore));
        }

        public void GenerateEmptyList(List<MonthlyDriverStats> monthlyDriverStats)
        {
            MonthlyDriverStats yearStats = new MonthlyDriverStats("", 0, 0, 0);
            MonthlyDriverStats januaryStats = new MonthlyDriverStats("January", 0, 0, 0);
            MonthlyDriverStats februaryStats = new MonthlyDriverStats("February", 0, 0, 0);
            MonthlyDriverStats marchStats = new MonthlyDriverStats("March", 0, 0, 0);
            MonthlyDriverStats aprilStats = new MonthlyDriverStats("April", 0, 0, 0);
            MonthlyDriverStats mayStats = new MonthlyDriverStats("May", 0, 0, 0);
            MonthlyDriverStats juneStats = new MonthlyDriverStats("June", 0, 0, 0);
            MonthlyDriverStats julyStats = new MonthlyDriverStats("July", 0, 0, 0);
            MonthlyDriverStats augustStats = new MonthlyDriverStats("August", 0, 0, 0);
            MonthlyDriverStats septemberStats = new MonthlyDriverStats("September", 0, 0, 0);
            MonthlyDriverStats octoberStats = new MonthlyDriverStats("October", 0, 0, 0);
            MonthlyDriverStats novemberStats = new MonthlyDriverStats("November", 0, 0, 0);
            MonthlyDriverStats decemberStats = new MonthlyDriverStats("December", 0, 0, 0);

            monthlyDriverStats.Add(yearStats);
            monthlyDriverStats.Add(januaryStats);
            monthlyDriverStats.Add(februaryStats);
            monthlyDriverStats.Add(marchStats);
            monthlyDriverStats.Add(aprilStats);
            monthlyDriverStats.Add(mayStats);
            monthlyDriverStats.Add(juneStats);
            monthlyDriverStats.Add(julyStats);
            monthlyDriverStats.Add(augustStats);
            monthlyDriverStats.Add(septemberStats);
            monthlyDriverStats.Add(octoberStats);
            monthlyDriverStats.Add(novemberStats);
            monthlyDriverStats.Add(decemberStats);
        }

        public void SortStats(List<MonthlyDriverStats> monthlyDriverStats2023, List<MonthlyDriverStats> monthlyDriverStats2022, List<MonthlyDriverStats> monthlyDriverStats2021)
        {
            foreach (FinishedRide finishedRide in FinishedRides)
            {
                if (finishedRide.Ride.DateTime.Year == 2023)
                {
                    if (finishedRide.Ride.DateTime.Month == 1)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "January")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 2)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "February")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 3)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "March")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 4)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "April")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 5)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "May")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 6)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "June")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 7)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "July")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 8)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "August")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 9)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "September")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 10)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "October")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 11)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "November")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 12)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "December")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                }
                else if (finishedRide.Ride.DateTime.Year == 2022)
                {
                    if (finishedRide.Ride.DateTime.Month == 1)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "January")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 2)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "February")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 3)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "March")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 4)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "April")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 5)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "May")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 6)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "June")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 7)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "July")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 8)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "August")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 9)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "September")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 10)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "October")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 11)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "November")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 12)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "December")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                }
                else if (finishedRide.Ride.DateTime.Year == 2021)
                {
                    if (finishedRide.Ride.DateTime.Month == 1)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "January")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 2)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "February")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 3)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "March")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 4)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "April")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 5)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "May")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 6)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "June")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 7)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "July")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 8)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "August")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 9)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "September")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 10)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "October")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 11)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "November")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                    if (finishedRide.Ride.DateTime.Month == 12)
                    {
                        foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
                        {
                            if (monthlyDriverStats.Month == "December")
                            {
                                monthlyDriverStats.TotalRides++;
                                monthlyDriverStats.AveragePrice += double.Parse(finishedRide.Price.Split(' ')[0]);
                                monthlyDriverStats.AverageTime += TimeSpan.Parse(finishedRide.Time).TotalSeconds;
                            }
                        }
                    }
                }
            }

            int sumTotalRides2023 = 0;
            double sumPrice2023 = 0;
            double sumTime2023 = 0;
            int sumTotalRides2022 = 0;
            double sumPrice2022 = 0;
            double sumTime2022 = 0;
            int sumTotalRides2021 = 0;
            double sumPrice2021 = 0;
            double sumTime2021 = 0;

            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
            {
                if (monthlyDriverStats.TotalRides != 0)
                {
                    sumTotalRides2023 += monthlyDriverStats.TotalRides;
                    sumPrice2023 += monthlyDriverStats.AveragePrice;
                    sumTime2023 += monthlyDriverStats.AverageTime;

                    monthlyDriverStats.AveragePrice = monthlyDriverStats.AveragePrice / monthlyDriverStats.TotalRides;
                    monthlyDriverStats.AverageTime = monthlyDriverStats.AverageTime / monthlyDriverStats.TotalRides;
                }
            }
            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2022)
            {
                if (monthlyDriverStats.TotalRides != 0)
                {
                    sumTotalRides2022 += monthlyDriverStats.TotalRides;
                    sumPrice2022 += monthlyDriverStats.AveragePrice;
                    sumTime2022 += monthlyDriverStats.AverageTime;

                    monthlyDriverStats.AveragePrice = monthlyDriverStats.AveragePrice / monthlyDriverStats.TotalRides;
                    monthlyDriverStats.AverageTime = monthlyDriverStats.AverageTime / monthlyDriverStats.TotalRides;
                }
            }
            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2021)
            {
                if (monthlyDriverStats.TotalRides != 0)
                {
                    sumTotalRides2021 += monthlyDriverStats.TotalRides;
                    sumPrice2021 += monthlyDriverStats.AveragePrice;
                    sumTime2021 += monthlyDriverStats.AverageTime;

                    monthlyDriverStats.AveragePrice = monthlyDriverStats.AveragePrice / monthlyDriverStats.TotalRides;
                    monthlyDriverStats.AverageTime = monthlyDriverStats.AverageTime / monthlyDriverStats.TotalRides;
                }
            }

            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2023)
            {
                if (monthlyDriverStats.Month == "")
                {
                    monthlyDriverStats.TotalRides = sumTotalRides2023;
                    if (monthlyDriverStats.TotalRides != 0)
                    {
                        monthlyDriverStats.AveragePrice = sumPrice2023 / sumTotalRides2023;
                        monthlyDriverStats.AverageTime = sumTime2023 / sumTotalRides2023;
                    }
                }
            }
            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2022)
            {
                if (monthlyDriverStats.Month == "")
                {
                    monthlyDriverStats.TotalRides = sumTotalRides2022;
                    if (monthlyDriverStats.TotalRides != 0)
                    {
                        monthlyDriverStats.AveragePrice = sumPrice2022 / sumTotalRides2022;
                        monthlyDriverStats.AverageTime = sumTime2022 / sumTotalRides2022;
                    }
                }
            }
            foreach (MonthlyDriverStats monthlyDriverStats in MonthlyDriverStats2021)
            {
                if (monthlyDriverStats.Month == "")
                {
                    monthlyDriverStats.TotalRides = sumTotalRides2021;
                    if (monthlyDriverStats.TotalRides != 0)
                    {
                        monthlyDriverStats.AveragePrice = sumPrice2021 / sumTotalRides2021;
                        monthlyDriverStats.AverageTime = sumTime2021 / sumTotalRides2021;
                    }
                }
            }
        }

        private INavigationService CreateCloseStatsNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

    }
}
