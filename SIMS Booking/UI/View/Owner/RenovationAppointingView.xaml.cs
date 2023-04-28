using SIMS_Booking.UI.ViewModel.Owner;
using System.Collections.Generic;
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
        }

        private void SubscribeToBlackoutDatesChangedEvent(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = (RenovationAppointingViewModel)DataContext;
            if (viewModel != null)
                viewModel.BlackoutDatesChangedEvent += UpdateBlackoutDates;
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
    }
}
