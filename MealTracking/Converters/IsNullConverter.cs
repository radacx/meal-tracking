using System;
using System.Globalization;
using System.Windows.Data;
using MealTracking.Converters.Common;

namespace MealTracking.Converters
{
    internal class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = value == null;
            var param = ConverterUtils.GetConverterParameter(parameter);

            return param == ConverterParameter.Negation
                ? !result
                : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}