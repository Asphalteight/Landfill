using Landfill.DataAccess.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class PersonFullNameToInitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var person = (PersonModel)value;
            var initials = $"{person.LastName} {person.FirstName.FirstOrDefault()}.";
            if (person.MiddleName != null)
            {
                initials += $" {person.MiddleName.FirstOrDefault()}.";
            }
            return initials;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
