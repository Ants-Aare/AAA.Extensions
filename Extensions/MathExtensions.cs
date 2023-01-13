using System;
using UnityEngine;

namespace AAA.Extensions
{
    public static class MathExtensions
    {
        public static bool AreEpsilonEquals(float a, float b)
        {
            return Mathf.Abs(a - b) < Mathf.Epsilon;
        }

        public static bool IsEpsilonEqualsZero(float a)
        {
            return Mathf.Abs(a) < Mathf.Epsilon;
        }

        public static float GetNormalizedFactor(int current, int max)
        {
            if (current == max)
            {
                return 1f;
            }

            current = Math.Max(0, current);
            current = Math.Min(current, max);

            return Divide(current, max);
        }

        public static float GetNormalizedFactor(float current, float max)
        {
            return GetNormalizedFactor(current, 0, max);
        }

        public static float GetNormalizedFactor(float current, float min, float max)
        {
            current = Math.Max(min, current);
            current = Math.Min(current, max);

            float maxMinusMin = max - min;

            return Divide(current, maxMinusMin);
        }

        public static float Divide(float a, float b)
        {
            if (b == 0f)
            {
                return 0;
            }

            return a / b;
        }

        public static float Divide(int a, int b)
        {
            if (b == 0)
            {
                return 0;
            }

            return (float)a / b;
        }

        /// <summary>
        /// Calculates hypotenuse using Pitagoras's algorithm
        /// </summary>
        public static float Hypotenuse(float side1, float side2)
        {
            return Mathf.Sqrt(side1 * side1 + side2 * side2);
        }

        /// <summary>
        /// Calculates the summation for a positive integer value value which is  sum of all numbers from 1 to that number
        /// </summary>
        public static int Summation1ToN(int value)
        {
            return (value * (value + 1)) / 2;
        }
    }
}
