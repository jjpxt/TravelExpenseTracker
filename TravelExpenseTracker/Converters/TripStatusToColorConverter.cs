using System.Globalization;
using TravelExpenseTracker.Shared;

namespace TravelExpenseTracker.Converters;

public class TripStatusToColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if(value is string tripStatus)
        {
            var statusColor = tripStatus switch
            {
                nameof(TripStatus.Planned) => Colors.Green,
                nameof(TripStatus.Ongoing) => Colors.Orange,
                nameof(TripStatus.Completed) => Colors.Blue,
                nameof(TripStatus.Cancelled) => Colors.Red,
            };
            return statusColor;
        }
        return Colors.Black;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
