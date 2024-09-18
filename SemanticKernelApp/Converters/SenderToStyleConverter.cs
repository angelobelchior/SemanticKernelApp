using System.Globalization;

namespace SemanticKernelApp.Converters;

public class SenderToStyleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return value;
        var resources = Application.Current?.Resources.MergedDictionaries.ElementAt(1);
        if (resources is null) return value;
        if (resources.Count == 0) return value;

        var sender = (Models.Sender)value;
        return sender == Models.Sender.User
            ? resources["UserMessageStyle"]
            : resources["SystemMessageStyle"];
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value;
}