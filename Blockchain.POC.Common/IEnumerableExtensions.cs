using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable<object> list)
        {
            return list == null || !list.Any();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }
    }
}