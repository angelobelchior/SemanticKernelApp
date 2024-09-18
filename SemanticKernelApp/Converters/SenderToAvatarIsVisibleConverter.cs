using System.Globalization;
using SemanticKernelApp.Models;

namespace SemanticKernelApp.Converters;

public class SenderToAvatarIsVisibleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return false;
        var sender = (Sender)value;
        return sender == Sender.Agent;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => false;
}