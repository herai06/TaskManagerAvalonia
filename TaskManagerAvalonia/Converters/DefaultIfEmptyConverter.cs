using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace TaskManagerAvalonia.Converters
{
    public class DefaultIfEmptyConverter : IValueConverter
    {
        public object? Convert(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            return value;
        }

        public object? ConvertBack(
            object? value,
            Type targetType,
            object? parameter,
            CultureInfo culture
        )
        {
            return string.IsNullOrWhiteSpace(value as string) ? parameter : value;
        }
    }
}
