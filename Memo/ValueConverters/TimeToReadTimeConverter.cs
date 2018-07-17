using System;
using System.Globalization;
using System.Windows;

namespace Memo
{
    /// <summary>
    /// A converter that takes in a date and converts it to a more user-friendly format for the message read time
    /// </summary>
    public class TimeToReadTimeConverter : BaseValueConverter<TimeToReadTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (DateTimeOffset)value;

            // If the message hasn't been read...
            if (time == DateTimeOffset.MinValue)
                // Return an empty string (so nothing is shown)
                return string.Empty;

            // If it is today...
            if (time.Date == DateTimeOffset.UtcNow.Date)
                // Return time
                return $"Read {time.ToLocalTime().ToString("HH:mm")}";

            // Else, return a full date
            return $"Read {time.ToLocalTime().ToString("HH:mm, dd MMM")}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
