using System;
using System.Collections.Generic;

namespace AAA.Extensions
{
    public static class ListExtensions
    {
        public static void AddRangeIfDoesNotContain<T>(this List<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                var contains = list.Contains(item);

                if (contains)
                {
                    continue;
                }

                list.Add(item);
            }
        }

        public static List<TConverted> CastAll<T, TConverted>(this List<T> list) where TConverted : class
        {
            return list.ConvertAll(x => x as TConverted);
        }

        /// <summary>
        /// Inserts an element at the position the func becomes true for the first time or at the end
        /// </summary>
        /// <param name="list">The list to apply this to</param>
        /// <param name="element">The element to insert</param>
        /// <param name="shouldAddAtPositionFunc">First element is the element to insert, second is the list element</param>
        /// <typeparam name="T">The type of the list</typeparam>
        public static int InsertByFunc<T>(this List<T> list, T element, Func<T, T, bool> shouldAddAtPositionFunc)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (shouldAddAtPositionFunc.Invoke(element, list[i]))
                {
                    list.Insert(i, element);
                    return i;
                }
            }

            list.Add(element);
            return list.Count - 1;
        }

        public static bool TryPop<T>(this List<T> list, out T value)
        {
            var couldGet = list.TryGet(0, out value);

            if (!couldGet)
            {
                return false;
            }

            list.RemoveAt(0);
            return true;
        }
        public static T Pop<T>(this List<T> list)
        {
            if (list.Count == 0)
                throw new InvalidOperationException("Attempting to pop item on empty list.");
            var index = list.Count - 1;
            var obj = list[index];
            list.RemoveAt(index);
            return obj;
        }

        public static void RemoveAll<T>(this List<T> list, T value)
        {
            for (var i = list.Count - 1; i >= 0; --i)
            {
                var checkingValue = list[i];

                if (checkingValue.Equals(value))
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}
