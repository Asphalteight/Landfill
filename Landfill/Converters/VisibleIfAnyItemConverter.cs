using Landfill.Common.Enums;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class RolesListToVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var roles = (RoleEnum[])value;
            var isVisible = roles.Any(x => x == RoleEnum.Admin || x == RoleEnum.Manager);
            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
