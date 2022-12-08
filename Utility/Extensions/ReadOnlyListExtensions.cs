using System.Collections.Generic;
using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class ReadOnlyListExtensions
    {
        public static bool TryGet<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            bool outsideBounds = index < 0 || list.Count <= index;

            if (outsideBounds)
            {
                item = default;
                return false;
            }

            item = list[index];
            return true;
        }

        public static int ClampIndex<T>(this IReadOnlyList<T> list, int index)
        {
            return Mathf.Clamp(index, 0, list.Count - 1);
        }

        public static bool TryGetClamped<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            index = list.ClampIndex(index);

            return list.TryGet(index, out item);
        }

        public static bool TryGetItemIndex<T>(this IReadOnlyList<T> list, T item, out int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T checkingItem = list[i];

                if (!checkingItem.Equals(item))
                {
                    continue;
                }

                index = i;
                return true;
            }

            index = default;
            return false;
        }
    }
}
