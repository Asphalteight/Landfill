using System;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class ComparisonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result;
            if (parameter == null && value == null)
            {
                result = true;
            }
            else
            {
                result = value?.Equals(parameter) ?? false;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = value?.Equals(true) == true ? parameter : Binding.DoNothing;
            return result;
        }
    }
}
