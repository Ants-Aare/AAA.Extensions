using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AAA.Extensions
{
    public static class FloatExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TimeSpan ToSeconds(this float value)
        {
            return TimeSpan.FromSeconds(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToVector3XYZ(this float value)
        {
            return new Vector3(value, value, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(this float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBetween(this float value, float minInclusive, float maxInclusive)
        {
            return (value >= minInclusive && value <= maxInclusive) || (value <= minInclusive && value >= maxInclusive);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetFractionalPart(this float value) => value - ((int)value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIntegerPart(this float value) => (int)value;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int, float) SplitIntegerAndFractionalParts(this float value) => ((int)value, value - ((int)value));
    }
}