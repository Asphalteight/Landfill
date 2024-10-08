﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class DateOnlyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = "";
            if (value != null)
            {
                result = ((DateTime)value).Date.ToShortDateString();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
