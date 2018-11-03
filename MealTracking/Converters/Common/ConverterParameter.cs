namespace MealTracking.Converters.Common
{
    internal static class ConverterUtils
    {
        public static ConverterParameter GetConverterParameter(object obj)
        {
            if (obj is ConverterParameter param)
            {
                return param;
            }

            return ConverterParameter.None;
        }
    }
    
    internal enum ConverterParameter
    {
        None,
        Negation,
    }
}