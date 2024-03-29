using System;
using AAA.Extensions;
using UnityEngine;

namespace AAA.Extensions
{
    public static class Vector2Extensions
    {
        public static readonly Vector2 MaxValue = new(float.MaxValue, float.MaxValue);
        public static readonly Vector2 MinValue = new(float.MinValue, float.MinValue);
        public static readonly Vector2 HalfOne = new(0.5f, 0.5f);

        public static Vector2 GetClosestPointOnFiniteLine(this Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            var heading = (lineEnd - lineStart);
            var magnitudeMax = heading.magnitude;
            heading.Normalize();

            var lhs = point - lineStart;
            var dotP = Vector2.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return lineStart + heading * dotP;
        }
        public static Vector2 GetClosestPointOnInfiniteLine(this Vector2 point, Vector2 origin, Vector2 direction)
        {
            direction.Normalize();
            var lhs = point - origin;

            var dotP = Vector2.Dot(lhs, direction);
            return origin + direction * dotP;
        }
        
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
            var sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            var cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            var tx = v.x;
            var ty = v.y;
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

        public static Vector2 Clamp(this Vector2 value, Vector2 minValue, Vector2 maxValue)
            => Vector2.Min(Vector2.Max(minValue, value), maxValue);

        public static Vector2 ClampX(this Vector2 value, float minValue, float maxValue)
            => new(Math.Clamp(value.x, minValue, maxValue), value.y);

        public static Vector2 ClampY(this Vector2 value, float minValue, float maxValue)
            => new(value.x, Math.Clamp(value.y, minValue, maxValue));

        public static Vector2 SetX(this Vector2 vector, float value)
            => new(value, vector.y);

        public static Vector2 SetY(this Vector2 vector, float value)
            => new(vector.x, value);

        public static Vector3 SetZ(this Vector2 vector, float value)
            => new(vector.x,vector.y, value);

        public static bool IsContained(this Vector2 vector, Vector2 minVector, Vector2 maxVector)
            => vector.x > minVector.x
               && vector.y > minVector.y
               && vector.x < maxVector.x
               && vector.y < maxVector.y;

        public static bool XYIsEqual(this Vector2 vector)
            => vector.x == vector.y
                ? true
                : Mathf.Approximately(vector.x, vector.y);

        public static bool IsGreaterThan(this Vector2 vector, Vector2 value)
        {
            return vector.x > value.x && vector.y > value.y;
        }

        public static bool IsAnyGreaterThan(this Vector2 vector, Vector2 value)
        {
            return vector.x > value.x || vector.y > value.y;
        }

        public static bool IsLesserThan(this Vector2 vector, Vector2 value)
        {
            return vector.x < value.x && vector.y < value.y;
        }

        public static bool IsAnyLesserThan(this Vector2 vector, Vector2 value)
        {
            return vector.x < value.x || vector.y < value.y;
        }


        #region Screen Utility Extensions

        public static Vector2 ScreenToViewPort(this Vector2 vector)
        {
            vector.x /= Screen.width;
            vector.y /= Screen.height;
            return vector;
        }

        public static Vector2 ViewportToScreen(this Vector2 vector)
        {
            vector.x *= Screen.width;
            vector.y *= Screen.height;
            return vector;
        }

        /// <summary>
        /// This will make the distance of the vector not based on aspect ratio. The y value may go above 1.
        /// </summary>
        public static Vector2 FixViewportDistanceHeight(this Vector2 vector)
        {
            vector.y *= Screen.height;
            vector.y /= Screen.width;
            return vector;
        }

        /// <summary>
        /// This will make the distance of the vector not based on aspect ratio. The x value may go above 1.
        /// </summary>
        public static Vector2 FixViewportDistanceWidth(this Vector2 vector)
        {
            vector.x *= Screen.width;
            vector.x /= Screen.height;
            return vector;
        }

        #endregion
    }
}