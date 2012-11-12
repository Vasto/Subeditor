using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Utilities
{
    public static class IEnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

    }
}
