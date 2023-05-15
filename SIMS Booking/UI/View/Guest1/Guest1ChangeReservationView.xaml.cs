using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.ViewModel.Guest1;

namespace SIMS_Booking.UI.View.Guest1
{
    public partial class Guest1ChangeReservationView : UserControl
    {
        public Guest1ChangeReservationView()
        {
            InitializeComponent();
            DataContextChanged += SubscribeToBlackoutDatesChangedEvent;
            DataContextChanged += SubscribeToEndDateChangedEvent;
            startDateDp.DisplayDateStart = DateTime.Today.AddDays(1);
        }


        private void SubscribeToBlackoutDatesChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (Guest1ChangeReservationViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.BlackoutDatesChangedEvent += UpdateBlackoutDates;
                viewModel.DisableReservedDates();
            }
        }

        private void SubscribeToEndDateChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (Guest1ChangeReservationViewModel)DataContext;
            if (viewModel != null)
            {
                viewModel.EndDpDateStartChangedEvent += UpdateEndDates;
            }
        }

        private void UpdateEndDates(int minDays)
        {
            if (startDateDp.SelectedDate.HasValue)
            {
                endDateDp.IsEnabled = true;
                endDateDp.DisplayDateStart = startDateDp.SelectedDate.Value.AddDays(minDays);
                endDateDp.SelectedDate = startDateDp.SelectedDate.Value.AddDays(minDays);
            }
        }

        //metoda koja onemogucuje rezervisane datume na kalendaru
        private void UpdateBlackoutDates(List<CalendarDateRange> blackoutDates)
        {
            startDateDp.BlackoutDates.Clear();
            foreach (var blackoutDate in blackoutDates)
            {
                startDateDp.BlackoutDates.Add(blackoutDate);
            }
        }
    }
}
