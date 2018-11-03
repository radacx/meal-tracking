using System.Collections.Generic;
using System.Linq;

namespace MealTracking.Contract.Extensions
{
    public static class CollectionExtensions
    {
        public static int FindIndexByReference<T>(this ICollection<T> collection, T item)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                if (ReferenceEquals(collection.ElementAt(i), item))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public static void RemoveByReference<T>(this IList<T> collection, T item)
        {
            var index = collection.FindIndexByReference(item);

            if (index == -1)
            {
                return;
            }
            
            collection.RemoveAt(index);
        }
    }
}