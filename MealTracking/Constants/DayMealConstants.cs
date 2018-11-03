using System.Collections.Generic;
using System.Linq;

namespace MealTracking.Constants
{
    public class DayMealConstants
    {
        public const int MinimumAllowedNameLength = 1;
        public const int MaximumAllowedNameLength = 60;

        public static readonly IEnumerable<int> Hours = Enumerable.Range(0, 24);
    }
}