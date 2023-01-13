using System;
using UnityEngine;

namespace AAA.Extensions
{
    public static class Vector2IntExtensions
    {
        public static readonly Vector2Int MaxValue = new(int.MaxValue, int.MaxValue);
        public static readonly Vector2Int MinValue = new(int.MinValue, int.MinValue);

        public static Vector2Int PerpendicularClockwise(this Vector2Int vector)
            => new(vector.y, -vector.x);

        public static Vector2Int PerpendicularCounterClockwise(this Vector2Int vector)
            => new(-vector.y, vector.x);

        public static Vector2Int Normalized(this Vector2Int vector)
            => new(Math.Sign(vector.x), Math.Sign(vector.y));

        public static Vector2Int SwapAxis(this Vector2Int vector)
            => new(vector.y, vector.x);

        public static Vector2 ToVector2(this Vector2Int vector)
            => new(vector.x, vector.y);

        public static Vector3 ToVector3XY(this Vector2Int vector)
            => new(vector.x, vector.y);

        public static Vector3 ToVector3XZ(this Vector2Int vector2Int)
            => new(vector2Int.x, 0f, vector2Int.y);

        public static Vector3Int ToVector3IntXY(this Vector2Int vector)
            => new(vector.x, vector.y);

        public static Vector3Int ToVector3IntXZ(this Vector2Int vector)
            => new(vector.x, 0, vector.y);

        public static Vector2Int SetX(this Vector2Int vector, int value)
            => new Vector2Int(value, vector.y);

        public static Vector2Int SetY(this Vector2Int vector, int value)
            => new Vector2Int(vector.x, value);
    }
}