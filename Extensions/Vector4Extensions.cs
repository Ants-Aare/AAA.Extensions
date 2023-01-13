using AAA.Extensions;
using UnityEngine;

namespace AAA.Extensions
{
    public static class Vector4Extensions
    {
        public static readonly Vector4 MaxValue = new(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
        public static readonly Vector4 MinValue = new(float.MinValue, float.MinValue, float.MinValue, float.MinValue);
        public static readonly Vector4 HalfOne = new(0.5f, 0.5f, 0.5f, 0.5f);

        public static bool AreEpsilonEquals(Vector4 a, Vector4 b)
            => MathExtensions.IsEpsilonEqualsZero((a - b).sqrMagnitude);

        public static bool IsEpsilonEqualsZero(Vector4 a)
            => MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);

        public static Vector4 Abs(this Vector4 vector)
            => new(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z), Mathf.Abs(vector.w));

        public static Vector4 Reciprocal(this Vector4 vector)
            => new(1f/vector.x, 1f/vector.y, 1f/vector.z, 1f/vector.w);

        public static Vector4 Clamp(Vector4 value, Vector4 minValue, Vector4 maxValue)
            => Vector4.Min(Vector4.Max(minValue, value), maxValue);

        public static float MaxComponent(this Vector4 value)
            => Mathf.Max(Mathf.Max(value.x, value.y), Mathf.Max(value.z, value.w));

        public static float MinComponent(this Vector4 value)
            => Mathf.Min(Mathf.Min(value.x, value.y), Mathf.Min(value.z, value.w));
    }
}
