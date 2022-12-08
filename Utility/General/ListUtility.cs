using System;
using System.Collections.Generic;
using System.Linq;

namespace AAA.Utility.General
{
    public static class ListUtility
    {
        public static T Pop<T>(this IList<T> list)
        {
            if (!list.Any<T>())
                throw new InvalidOperationException("Attempting to pop item on empty list.");
            int index = list.Count - 1;
            T obj = list[index];
            list.RemoveAt(index);
            return obj;
        }
    }
}