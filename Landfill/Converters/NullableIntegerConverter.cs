using Landfill.Common.Helpers;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class NullableIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (string)value;
            return stringValue.IsNullOrWhiteSpace() ? null : int.Parse(stringValue);
        }
    }
}
