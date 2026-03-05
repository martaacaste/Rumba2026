using System.Globalization;
using Avalonia.Data.Converters;

namespace PacManVersion;

public class MultiplyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double d && parameter is string s && double.TryParse(s, out double factor))
        {
            return d * factor;
        }
        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}