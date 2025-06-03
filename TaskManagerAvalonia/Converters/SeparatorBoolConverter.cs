﻿using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TaskManagerAvalonia.Converters
{
    internal class SeparatorBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return true;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            throw new NotImplementedException();
        }
    }
}
