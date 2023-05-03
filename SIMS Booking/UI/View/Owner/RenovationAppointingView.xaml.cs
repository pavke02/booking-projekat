using SIMS_Booking.UI.ViewModel.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class RenovationAppointingView : UserControl
    {
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
                endDatesCalendar.IsEnabled=true;
                endDatesCalendar.DisplayDateStart = startDatesCalendar.SelectedDate.Value.AddDays(1);
                endDatesCalendar.DisplayDateEnd = startDatesCalendar.BlackoutDates.FirstOrDefault(d => d.Start > startDatesCalendar.SelectedDate.Value)?.Start.AddDays(-1);
            }
        }
    }
}
