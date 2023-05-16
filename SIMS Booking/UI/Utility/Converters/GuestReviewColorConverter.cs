
using System.Globalization;
using System;
using System.Windows.Data;

namespace SIMS_Booking.UI.Utility.Converters
{
    internal class GuestReviewColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Now >= (DateTime)value && (DateTime.Now - (DateTime)value).TotalDays <= 5;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
