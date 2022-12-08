using System;
using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class Vector3IntExtensions
    {
        public static readonly Vector3Int MaxValue = new(int.MaxValue, int.MaxValue, int.MaxValue);
        public static readonly Vector3Int MinValue = new(int.MinValue, int.MinValue, int.MinValue);

        public static Vector3Int AbsManhattanDistance(Vector3Int a, Vector3Int b)
        {
            Vector3Int distance = b - a;

            return new Vector3Int(Mathf.Abs(distance.x), Mathf.Abs(distance.y), Mathf.Abs(distance.z));
        }

        public static Vector3Int SwapXZ(this Vector3Int value)
            => new(value.z, value.y, value.x);

        public static Vector3Int ToVector3Int(this Vector3 value)
            => new(Mathf.RoundToInt(value.x), Mathf.RoundToInt(value.y), Mathf.RoundToInt(value.z));

        public static Vector3 ToVector3XZY(this Vector3 value) => new(value.x, value.z, value.y);

        public static Vector3Int Normalized(this Vector3Int vector)
            => new(Math.Sign(vector.x), Math.Sign(vector.x), Math.Sign(vector.x));

        public static Vector3 ToVector3(this Vector3Int vector)
            => new(vector.x, vector.y, vector.z);

        public static Vector2 ToVector2XY(this Vector3Int vector)
            => new(vector.x, vector.y);

        public static Vector2Int ToVector2IntXY(this Vector3Int vector)
            => new(vector.x, vector.y);

        public static Vector2 ToVector2XZ(this Vector3Int vector)
            => new(vector.x, vector.z);

        public static Vector2Int ToVector2IntXZ(this Vector3Int vector)
            => new(vector.x, vector.z);

        public static Vector3Int SetX(this Vector3Int vector, int value)
            => new Vector3Int(value, vector.y, vector.z);

        public static Vector3Int SetY(this Vector3Int vector, int value)
            => new Vector3Int(vector.x, value, vector.z);

        public static Vector3Int SetZ(this Vector3Int vector, int value)
            => new Vector3Int(vector.x, vector.y, value);
    }
}