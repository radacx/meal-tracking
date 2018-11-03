using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MealTracking.Converters
{
    internal class TextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            if (!(value is string textValue))
            {
                return null;
            }

            return string.IsNullOrWhiteSpace(textValue) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}