using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AAA.Extensions
{
    public static class Vector3Extensions
    {
        public static readonly Vector3 MaxValue = new(float.MaxValue, float.MaxValue, float.MaxValue);
        public static readonly Vector3 MinValue = new(float.MinValue, float.MinValue, float.MinValue);
        public static readonly Vector3 HalfOne = new(0.5f, 0.5f, 0.5f);
        
        public static Vector3 GetClosestPointOnFiniteLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            var lineDirection = lineEnd - lineStart;
            var lineLength = lineDirection.magnitude;
            lineDirection.Normalize();
            var projectLength = Mathf.Clamp(Vector3.Dot(point - lineStart, lineDirection), 0f, lineLength);
            return lineStart + lineDirection * projectLength;
        }

        public static Vector3 GetClosestPointOnInfiniteLine(this Vector3 point, Vector3 lineStart, Vector3 lineEnd)
        {
            return lineStart + Vector3.Project(point - lineStart, lineEnd - lineStart);
        }
        
        public static bool AreEpsilonEquals(Vector3 a, Vector3 b)
            => MathExtensions.IsEpsilonEqualsZero((a - b).sqrMagnitude);

        public static bool IsEpsilonEqualsZero(Vector3 a)
            => MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);

        public static Vector3 Abs(this Vector3 vector)
            => new(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));

        public static float AbsDistance(Vector3 a, Vector3 b)
            => Mathf.Abs(Vector3.Distance(a, b));

        public static Vector3 Reciprocal(this Vector3 vector)
            => new(1f / vector.x, 1f / vector.y, 1f / vector.z);

        public static Vector3 PerpendicularClockwiseXY(this Vector3 vector)
            => new(vector.y, -vector.x, vector.z);

        public static Vector3 PerpendicularCounterClockwiseXY(this Vector3 vector)
            => new(-vector.y, vector.x, vector.z);

        public static Vector3 PerpendicularClockwiseXZ(this Vector3 vector)
            => new(vector.z, vector.y, -vector.x);

        public static Vector3 PerpendicularCounterClockwiseXZ(this Vector3 vector)
            => new(-vector.z, vector.y, vector.x);

        public static Vector3 PerpendicularClockwiseYZ(this Vector3 vector)
            => new(vector.x, vector.z, -vector.y);

        public static Vector3 PerpendicularCounterClockwiseYZ(this Vector3 vector)
            => new(vector.x, -vector.z, vector.y);

        public static Vector2 ToVector2XY(this Vector3 vector)
            => new(vector.x, vector.y);

        public static Vector2 ToVector2XZ(this Vector3 vector)
            => new(vector.x, vector.z);

        public static Vector2Int ToVector2IntXY(this Vector3 vector)
            => new((int)vector.x, (int)vector.y);

        public static Vector2Int ToVector2IntXZ(this Vector3 vector)
            => new((int)vector.x, (int)vector.z);

        public static Vector3 Clamp(Vector3 value, Vector3 minValue, Vector3 maxValue)
            => Vector3.Min(Vector3.Max(minValue, value), maxValue);

        public static Vector3 Divide(this Vector3 numerator, Vector3 denominator)
            => new(
                MathExtensions.Divide(numerator.x, denominator.x),
                MathExtensions.Divide(numerator.y, denominator.y),
                MathExtensions.Divide(numerator.z, denominator.z)
            );

        public static Quaternion ToEulerQuaternion(this Vector3 value)
            => Quaternion.Euler(value.x, value.y, value.z);

        public static float MaxComponent(this Vector3 value)
            => Mathf.Max(value.x, Mathf.Max(value.x, value.y));

        public static float MinComponent(this Vector3 value)
            => Mathf.Min(value.x, Mathf.Min(value.y, value.z));

        public static Vector3 SetX(this Vector3 vector, float value) => new(value, vector.y, vector.z);

        public static Vector3 SetY(this Vector3 vector, float value) => new(vector.x, value, vector.z);

        public static Vector3 SetZ(this Vector3 vector, float value) => new(vector.x, vector.y, value);

        public static Vector3 RandomVector(Vector3 minVector, Vector3 maxVector)
            => new(
                Random.Range(minVector.x, maxVector.x),
                Random.Range(minVector.y, maxVector.y),
                Random.Range(minVector.z, maxVector.z)
            );

        public static Vector3 Get(this Vector3Direction direction)
            => direction switch
            {
                Vector3Direction.up => Vector3.up,
                Vector3Direction.down => Vector3.down,
                Vector3Direction.forward => Vector3.forward,
                Vector3Direction.back => Vector3.back,
                Vector3Direction.left => Vector3.left,
                Vector3Direction.right => Vector3.right,
                Vector3Direction.zero => Vector3.zero,
                Vector3Direction.one => Vector3.one,
                _ => Vector3.zero
            };
        
        
    }

    public enum Vector3Direction
    {
        zero = 0,
        one = 1,
        up,
        down,
        forward,
        back,
        left,
        right,
    }
}