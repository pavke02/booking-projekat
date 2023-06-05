using System;
using System.Globalization;
using System.Windows.Data;
using SIMS_Booking.Enums;

namespace SIMS_Booking.UI.Utility.Converters
{
    internal class ForumCommentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            return (CommentOwnerStatus)value == CommentOwnerStatus.Owner;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        { 
            throw new NotImplementedException();
        }
    }
}
