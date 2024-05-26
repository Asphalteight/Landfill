using System;
using System.Globalization;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";
            if (value != null)
            {
                var dateTime = (DateTime)value;
                result = dateTime.ToString("dd.MM.yyyy в HH:mm:ss"); 
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
