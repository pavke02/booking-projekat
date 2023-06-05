using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class RenovationAppointingView : UserControl
    {
        private Process process;

        public RenovationAppointingView()
        {
            InitializeComponent();
            DataContextChanged += SubscribeToBlackoutDatesChangedEvent;
            startDatesCalendar.DisplayDateStart = DateTime.Today.AddDays(1);
        }

        private void SubscribeToBlackoutDatesChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (RenovationAppointingViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.BlackoutDatesChangedEvent += UpdateBlackoutDates;
                viewModel.DisableReservedDates();
            }
        }

        //metoda koja onemogucuje rezervisane datume na kalendaru
        private void UpdateBlackoutDates(List<CalendarDateRange> blackoutDates)
        {
            startDatesCalendar.BlackoutDates.Clear();
            foreach (var blackoutDate in blackoutDates)
            {
                startDatesCalendar.BlackoutDates.Add(blackoutDate);
            }
        }

        //metoda koja kao opcije za kranji datum daje niz datuma od (selektovanog+1) do prvog (Blackout-ovanog-1)
        private void StartDateSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (startDatesCalendar.SelectedDate.HasValue)
            {
                endDatesCalendar.IsEnabled = true;
                endDatesCalendar.DisplayDateStart = startDatesCalendar.SelectedDate.Value.AddDays(1);
                endDatesCalendar.DisplayDateEnd = FindClosestUnavailableDate();
            }
        }

        private DateTime? FindClosestUnavailableDate()
        {
            List<CalendarDateRange> unavailableDateRanges = startDatesCalendar.BlackoutDates.Where(d => d.Start > startDatesCalendar.SelectedDate.Value).ToList();
            return unavailableDateRanges.OrderBy(a => a.Start).FirstOrDefault()?.Start.AddDays(-1);
        }

        private void ShowKeyboard(object sender, System.Windows.RoutedEventArgs e)
        {
            process = System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = "osk.exe", UseShellExecute = true });
        }

        private void HideKeyboard(object sender, System.Windows.RoutedEventArgs e)
        {
            process.Kill();
        }
    }
}
