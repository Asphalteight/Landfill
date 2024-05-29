using System;
using System.Globalization;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class DateTimeToDateOnlyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime?)value;
            if (dateTime.HasValue)
            {
                return dateTime.Value.Date;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = (DateTime?)value;
            if (dateTime.HasValue)
            {
                return dateTime.Value.Date;
            }
            return null;
        }
    }
}
