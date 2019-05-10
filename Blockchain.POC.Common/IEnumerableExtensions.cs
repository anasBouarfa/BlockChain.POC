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
    }
}