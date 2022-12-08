using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class ArrayExtensions
    {
        public static bool TryGet<T>(this T[] array, int index, out T item)
        {
            bool outsideBounds = index < 0 || array.Length <= index;

            if (outsideBounds)
            {
                item = default;
                return false;
            }

            item = array[index];
            return true;
        }

        public static int ClampIndex<T>(this T[] array, int index)
        {
            return Mathf.Clamp(index, 0, array.Length - 1);
        }

        public static bool TryGetClamped<T>(this T[] array, int index, out T item)
        {
            index = array.ClampIndex(index);

            return array.TryGet(index, out item);
        }
    }
}
