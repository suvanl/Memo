using System;
using System.Globalization;
using System.Windows;

namespace Memo
{
    /// <summary>
    /// A converter that takes in a date and converts it to a more user-friendly format
    /// </summary>
    public class TimeToDisplayTimeConverter : BaseValueConverter<TimeToDisplayTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (DateTimeOffset)value;

            // If it is today...
            if (time.Date == DateTimeOffset.UtcNow.Date)
                // Return time
                return time.ToLocalTime().ToString("HH:mm");

            // Else, return a full date
            return time.ToLocalTime().ToString("HH:mm, dd MMM");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
