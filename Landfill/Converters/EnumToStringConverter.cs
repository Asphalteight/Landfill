using Landfill.Common.Helpers;
using System;
using System.Linq;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as Enum).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enum.GetValues(parameter as Type).Cast<Enum>().FirstOrDefault(v => v.GetDescription() == value.ToString());
        }
    }
}
