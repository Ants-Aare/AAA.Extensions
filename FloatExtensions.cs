using System;
using UnityEngine;

namespace AAA.Extensions
{
    public static class FloatExtensions
    {
        public static TimeSpan ToSeconds(this float value)
        {
            return TimeSpan.FromSeconds(value);
        }

        public static Vector3 ToVector3XYZ(this float value)
        {
            return new Vector3(value, value, value);
        }

        public static float Round(this float value, int decimals)
        {
            return (float)Math.Round(value, decimals);
        }
    }
}
