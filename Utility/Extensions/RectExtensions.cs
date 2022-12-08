using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class RectExtensions
    {
        public static readonly Rect PositiveInfinity = FromCenterAndSize(Vector2.zero, Vector2.positiveInfinity);
        public static readonly Rect NegativeInfinity = FromCenterAndSize(Vector2.zero, Vector2.negativeInfinity);

        public static Rect FromCenterAndSize(Vector2 center, Vector2 size)
        {
            Vector2 halfSize = size * 0.5f;

            return new Rect(center - halfSize, size);
        }

        public static void SetSizeKeepingCenterPosition(this ref Rect rect, Vector2 size)
        {
            Vector2 center = rect.center;
            rect.size = size;
            rect.center = center;
        }
    }
}
