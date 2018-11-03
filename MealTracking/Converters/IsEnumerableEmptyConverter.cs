using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;
using MealTracking.Converters.Common;

namespace MealTracking.Converters
{
    internal class IsCollectionEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ICollection collection))
            {
                return null;
            }

            var result = collection.Count == 0;
            
            var converterParam = ConverterUtils.GetConverterParameter(parameter);

            return converterParam == ConverterParameter.Negation
                ? !result
                : result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}