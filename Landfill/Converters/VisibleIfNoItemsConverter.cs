﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Landfill.Converters
{
    public class VisibleIfNoItemsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int?)value == 0 ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
