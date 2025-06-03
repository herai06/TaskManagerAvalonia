using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace TaskManagerAvalonia.Converters;

internal class ImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null
            ? new Bitmap(
                AssetLoader.Open(new Uri("avares://TaskManagerAvalonia/Assets/profile.png"))
            )
            : new Bitmap(new MemoryStream((byte[])value));
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        throw new NotImplementedException();
    }
}
