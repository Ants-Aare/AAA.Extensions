using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class BoundsExtensions
    {
        public static readonly Bounds PositiveInfinity = new(Vector3.zero, Vector3.positiveInfinity);
        public static readonly Bounds NegativeInfinity = new(Vector3.zero, Vector3.negativeInfinity);

        /// <summary>
        /// Returns the bounds as a RectInt. It is unsafe because it will lose all the float precision when
        /// converting the values to int
        /// </summary>
        public static RectInt ToRectIntXYUnsafe(this Bounds bounds)
        {
            var position = bounds.min;
            var size = bounds.size;
            return new RectInt(
                new Vector2Int((int)position.x, (int)position.y),
                new Vector2Int((int)size.x, (int)size.y)
            );
        }

        public static Rect ToRectXY(this Bounds bounds)
        {
            var position = bounds.min;
            var size = bounds.size;
            return new Rect(
                position.ToVector2XY(),
                size.ToVector2XY()
            );
        }

        public static bool ContainsBounds(this Bounds bounds, Bounds target)
        {
            return bounds.Contains(target.min) && bounds.Contains(target.max);
        }

        public static Vector3 GetPivot(this Bounds bounds)
        {
            return Vector3Extensions.HalfOne - Vector3Extensions.Divide(bounds.center, bounds.size);
        }
    }
}
