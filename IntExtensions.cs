using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AAA.Extensions
{
    public static class IntExtensions
    {
        public static TimeSpan ToSeconds(this int value)
        {
            return TimeSpan.FromSeconds(value);
        }

        public static TimeSpan ToHours(this int value)
        {
            return TimeSpan.FromHours(value);
        }

        public static Vector3 ToVector3IntXYZ(this int value)
        {
            return new Vector3Int(value, value, value);
        }

        public static bool ToBool(this int value)
        {
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// Converts an integer to its correspoinding alphabetical value.
        /// When this value goes over the max letter inside the alphabet, it concatenates the result, creating a sequence.
        /// Only works with 0 and positive integers.
        /// </summary>
        /// <example>0 == A, 26 == Z, 27 == AA, 28 == AB, 53 == BA, 703 == AAA</example>
        public static string ToAlphabeticalId(this int value)
        {
            value += 1;

            if (value < 0)
            {
                return string.Empty;
            }

            const int alphabetCharactersCount = 26;

            // Small optimization if value has only one character
            if (value <= alphabetCharactersCount)
            {
                return Convert.ToChar(value + 64).ToString();
            }

            StringBuilder stringBuilder = new();

            List<int> toCheck = new()
            {
                value
            };

            while (toCheck.Count > 0)
            {
                var checking = toCheck[0];
                toCheck.RemoveAt(0);

                var div = checking / alphabetCharactersCount;
                var mod = checking % alphabetCharactersCount;
                if (mod == 0) { mod = alphabetCharactersCount; div--; }

                if (checking > alphabetCharactersCount)
                {
                    toCheck.Insert(0, mod);
                    toCheck.Insert(0, div);
                    continue;
                }

                var character = Convert.ToChar(checking + 64);

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        public static int NewRandom<T>(this int previousRandom, T[] array)
            => NewRandom(previousRandom, 0, array.Length);
        public static int NewRandom<T>(this int previousRandom, IEnumerable<T> enumerable)
            => NewRandom(previousRandom, 0, enumerable.Count());

        public static int NewRandom(this int previousRandom, int minInclusive, int maxExclusive)
        {
            if (maxExclusive - minInclusive == 1)
            {
                return minInclusive;
            }

            if (maxExclusive - minInclusive == 2)
            {
                return previousRandom == minInclusive
                    ? maxExclusive - 1
                    : minInclusive;
            }

            var iterations = 0;
            while (true)
            {
                var candidate = Random.Range(minInclusive, maxExclusive);
                if (candidate != previousRandom)
                    return candidate;
                iterations++;

                if (iterations > 100)
                    return candidate;
            }
        }
        
        public static bool IsBetween(this int value, int minInclusive, int maxExclusive)
        {
            return (value >= minInclusive && value < maxExclusive) || (value <= minInclusive && value > maxExclusive);
        }
    }
}
