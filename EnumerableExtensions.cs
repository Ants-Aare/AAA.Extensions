using System;
using System.Collections.Generic;
using System.Linq;

namespace AAA.Extensions
{
    public static class EnumerableExtensions
    {
        public delegate bool TrySelectDelegate<in TValue, TResult>(TValue value, out TResult result);

        /// <summary>
        /// Tries to map all the elements from from one type to the other and
        /// returns all the elements that were able to be mapped
        /// </summary>
        public static IEnumerable<TResult> SelectValid<TValue, TResult>(
            this IEnumerable<TValue> enumerable,
            TrySelectDelegate<TValue, TResult> trySelectDelegate)
        {
            foreach (var element in enumerable)
            {
                if (trySelectDelegate.Invoke(element, out var result))
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Tries to map all the elements from the source type to the destiny type and only returns true if it was able to map
        /// all the elements
        /// </summary>
        public static bool TrySelectAll<TValue, TResult>(
            this IEnumerable<TValue> enumerable,
            TrySelectDelegate<TValue, TResult> trySelectDelegate,
            out List<TResult> results)
        {
            results = new List<TResult>();
            foreach (var value in enumerable)
            {
                if (!trySelectDelegate(value, out var result))
                {
                    results = default;
                    return false;
                }

                results.Add(result);
            }

            return true;
        }

        public static IEnumerable<(T value, int index)> ZipIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select(ZipIndexFunc);
        }

        public static (T value, int index) ZipIndexFunc<T>(T value, int index)
        {
            return (value, index);
        }

        public static IEnumerable<(T value, bool isLast)> ZipIsLast<T>(this IEnumerable<T> enumerable)
        {
            T previousValue = default;
            using var enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            previousValue = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return (previousValue, false);
                previousValue = enumerator.Current;
            }

            yield return (previousValue, true);
        }

        public static T MinObject<T>(this IEnumerable<T> enumerable, Comparer<T> comparer)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default;
            }

            T best = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;

                if (comparer.Compare(best, element) < 0)
                {
                    continue;
                }

                best = element;
            }

            return best;
        }

        public static T MinObject<T, Y>(this IEnumerable<T> enumerable, Func<T, Y> toCompareFunc, Comparer<Y> comparer)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return default;
            }

            T best = enumerator.Current;
            Y bestValue = toCompareFunc.Invoke(best);

            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                var elementValue = toCompareFunc.Invoke(element);

                if (comparer.Compare(bestValue, elementValue) < 0)
                {
                    continue;
                }

                best = element;
                bestValue = elementValue;
            }

            return best;
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyCollection<T> readOnlyCollection)
            {
                return readOnlyCollection;
            }

            return enumerable.ToArray();
        }

        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyList<T> readOnlyCollection)
            {
                return readOnlyCollection;
            }

            return enumerable.ToArray();
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(o => o != null);
        }

        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            return enumerable.SelectMany(DelegateExtensions.Self);
        }

        public static IEnumerable<T> RepeatElements<T>(this IEnumerable<T> enumerable, int count)
            => enumerable.Select(x => Enumerable.Repeat(x, count)).Flatten();

        public static T MinOrDefault<T>(this IEnumerable<T> enumerable)
        {
            TryGetMin(enumerable, out var value);
            return value;
        }

        public static bool TryGetMin<T>(this IEnumerable<T> enumerable, out T value)
        {
            var readOnlyCollection = enumerable.ToReadOnlyCollection();
            if (readOnlyCollection.Count == 0)
            {
                value = default;
                return false;
            }

            value = readOnlyCollection.Min();
            return false;
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable)
        {
            TryGetMax(enumerable, out var value);
            return value;
        }

        public static bool TryGetMax<T>(this IEnumerable<T> enumerable, out T value)
        {
            var readOnlyCollection = enumerable.ToReadOnlyCollection();
            if (readOnlyCollection.Count == 0)
            {
                value = default;
                return false;
            }

            value = readOnlyCollection.Max();
            return false;
        }

        public static bool TryGetFirst<T>(
            this IEnumerable<T> enumerable,
            Predicate<T> predicate,
            out T element)
        {
            foreach (var i in enumerable)
            {
                if (predicate.Invoke(i))
                {
                    element = i;
                    return true;
                }
            }

            element = default;
            return false;
        }

        public static bool TryGetFirst<T>(
            this IEnumerable<T> enumerable,
            out T element)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                element = default;
                return false;
            }

            element = enumerator.Current;
            return true;
        }
    }
}
