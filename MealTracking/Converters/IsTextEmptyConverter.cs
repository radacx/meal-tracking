using System;
using System.Globalization;
using System.Windows.Data;
using MealTracking.Converters.Common;

namespace MealTracking.Converters
{
    internal class IsTextEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result;

            switch (value) {
                case null:
                    result = true;

                    break;
                case string text:
                    result = string.IsNullOrWhiteSpace(text);

                    break;
                default:

                    return null;
            }
            
            var converterParam = ConverterUtils.GetConverterParameter(parameter);

            return converterParam == ConverterParameter.Negation
                ? !result
                : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}