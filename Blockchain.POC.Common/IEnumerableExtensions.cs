using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
