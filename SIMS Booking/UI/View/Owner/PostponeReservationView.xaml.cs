using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.UI.ViewModel.Owner;

namespace SIMS_Booking.UI.View.Owner
{
    public partial class PostponeReservationView : UserControl
    {
        public PostponeReservationView()
        {
            InitializeComponent();
            //subscribe se na SubscribeToBlackoutDatesChangedEvent
            DataContextChanged += SubscribeToBlackoutDatesChangedEvent;
        }

        ~PostponeReservationView()
        {
            var viewModel = (PostponeReservationViewModel)DataContext;
            if (viewModel != null)
                viewModel.BlackoutDatesChangedEvent -= UpdateBlackoutDates;
        }

        private void SubscribeToBlackoutDatesChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (PostponeReservationViewModel)DataContext;
            if (viewModel != null)
                viewModel.BlackoutDatesChangedEvent += UpdateBlackoutDates;
        }

        //metoda koja onemogucuje rezervisane datume na kalendaru
        private void UpdateBlackoutDates(List<CalendarDateRange> blackoutDates)
        {
            reservationCalendar.BlackoutDates.Clear();
            foreach (var item in blackoutDates)
            {
                reservationCalendar.BlackoutDates.Add(item);
            }
        }
    }
}
