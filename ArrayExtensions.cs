using UnityEngine;

namespace AAA.Extensions
{
    public static class ArrayExtensions
    {
        public static bool TryGet<T>(this T[] array, int index, out T item)
        {
            var outsideBounds = index < 0 || array.Length <= index;

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
            if (array == null)
                return -1;
            return Mathf.Clamp(index, 0, array.Length - 1);
        }

        public static bool TryGetClamped<T>(this T[] array, int index, out T item)
        {
            index = array.ClampIndex(index);

            return array.TryGet(index, out item);
        }

        public static T GetClamped<T>(this T[] array, int index)
        {
            if (array.Length == 0)
                return default;
            
            index = array.ClampIndex(index);
            return array[index];
        }

        public static int GetIndexLooped<T>(this T[] array, int index)
        {
            if (array == null)
                return -1;
            if (array.Length == 0)
                return -1;
            return index % array.Length;
        }

        public static T[] SetLength<T>(this T[] array, int newLength)
        {
            if (array.Length == newLength)
                return array;

            var newArray = new T[newLength];
            var arrayLength = array.Length;
            
            for (var i = 0; i < newLength; i++)
            {
                newArray[i] = i < arrayLength
                    ? array[i]
                    : default;
            }

            return newArray;
        }
        
        public static T GetRandom<T>(this T[] array)
        {
            var newIndex = Random.Range(0, array.Length);
            return array[newIndex];
        } 
        
        public static T GetNewRandom<T>(this T[] array, int previousRandom, out int selectedRandom)
        {
            if (array.Length == 1)
            {
                selectedRandom = 0;
                return array[0];
            }

            if (array.Length == 2)
            {
                selectedRandom = previousRandom == 0 ? 1 : 0;
                return array[selectedRandom];
            }

            var iterations = 0;
            while (true)
            {
                var candidate =Random.Range(0, array.Length);
                if (candidate != previousRandom)
                {
                    selectedRandom = candidate;
                    return array[selectedRandom];
                }

                iterations++;

                if (iterations > 100)
                {
                    selectedRandom = previousRandom;
                    return array[selectedRandom];
                }
            }
        }
    }
}