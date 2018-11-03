using System;
using System.Globalization;
using System.Windows.Data;
using MealTracking.Converters.Common;

namespace MealTracking.Converters
{
    internal class IsNumberZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result;
            
            switch (value)
            {    
                case int intNumber:

                    result = intNumber == 0;

                    break;
                case double doubleNumber:
                    result = Math.Abs(doubleNumber) < 0.0001d;

                    break;
                default:

                    return null;
            }
            
            var param = ConverterUtils.GetConverterParameter(parameter);

            return param == ConverterParameter.Negation
                ? !result
                : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}