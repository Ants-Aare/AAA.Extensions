using AAA.Extensions;
using UnityEngine;

namespace AAA.Extensions
{
    public static class Vector2Extensions
    {
        public static readonly Vector2 MaxValue = new(float.MaxValue, float.MaxValue);
        public static readonly Vector2 MinValue = new(float.MinValue, float.MinValue);
        public static readonly Vector2 HalfOne = new(0.5f, 0.5f);

        public static bool AreEpsilonEquals(Vector2 a, Vector2 b)
        {
            return MathExtensions.IsEpsilonEqualsZero((a - b).sqrMagnitude);
        }

        public static bool IsEpsilonEqualsZero(Vector2 a)
        {
            return MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }

        public static Vector2 Slerp(Vector2 a, Vector2 b, float factor)
        {
            var angle = Vector2.Angle(a, b);
            var angleFactor = angle * factor;
            var magnitude = Mathf.Lerp(a.magnitude, b.magnitude, factor);
            var vector = a.Rotate(angleFactor).normalized;
            var vectorWithMagnitude = vector * magnitude;
            return vectorWithMagnitude;
        }

        public static Vector2 MaxElements(Vector2 a, Vector2 b)
            => new(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));

        public static float MaxComponent(this Vector2 value)
            => Mathf.Max(value.x, value.y);

        public static float MinComponent(this Vector2 value)
            => Mathf.Min(value.x, value.y);

        public static Vector2 MinElements(Vector2 a, Vector2 b)
            => new(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));

        public static Vector2 Abs(this Vector2 vector)
            => new(Mathf.Abs(vector.x), Mathf.Abs(vector.y));

        public static Vector2 SwapAxis(this Vector2 vector)
            => new(vector.y, vector.x);

        public static Vector2 FlipX(this Vector2 vector)
            => new(-vector.x, vector.y);

        public static Vector2 FlipY(this Vector2 vector)
            => new(vector.x, -vector.y);

        public static Vector2 PerpendicularClockwise(this Vector2 vector)
            => new(vector.y, -vector.x);

        public static Vector2 PerpendicularCounterClockwise(this Vector2 vector)
            => new(-vector.y, vector.x);

        public static Vector3 ToVector3XY(this Vector2 vector)
            => new(vector.x, vector.y, 0f);

        public static Vector3 ToVector3XY(this Vector2 vector, float z)
            => new(vector.x, vector.y, z);

        public static Vector3 ToVector3XZ(this Vector2 vector)
            => new(vector.x, 0f, vector.y);

        public static Vector3 ToVector3XZ(this Vector2 vector, float y)
            => new(vector.x, y, vector.y);

        public static Vector2 Reciprocal(this Vector2 vector)
            => new(1f / vector.x, 1f / vector.y);

        public static Vector2 Clamp(Vector2 value, Vector2 minValue, Vector2 maxValue)
            => Vector2.Min(Vector2.Max(minValue, value), maxValue);

        public static Vector2 SetX(this Vector2 vector, float value)
            => new Vector2(value, vector.y);

        public static Vector2 SetY(this Vector2 vector, float value)
            => new Vector2(vector.x, value);
    }
}