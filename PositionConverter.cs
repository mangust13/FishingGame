using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FishingGame
{
    public class PositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is double leftPosition && values[1] is double topPosition)
            {
                return new Thickness(leftPosition, topPosition, 0, 0);
            }
            return new Thickness(0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
