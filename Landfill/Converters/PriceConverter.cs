using System;
using System.Globalization;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string priceString = value as string;
            if (priceString.Length > 2)
            {
                priceString = priceString[..^2];
            }

            if (decimal.TryParse(priceString, out var result))
            {
                return Math.Truncate(100*result)/100;
            }
            return null;
        }
    }
}
