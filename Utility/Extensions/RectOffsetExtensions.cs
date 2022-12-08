using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class RectOffsetExtensions
    {
        public static void Add(
            this RectOffset rectOffset,
            RectOffset otherRectOffset)
        {
            rectOffset.top += otherRectOffset.top;
            rectOffset.bottom += otherRectOffset.bottom;
            rectOffset.left += otherRectOffset.left;
            rectOffset.right += otherRectOffset.right;
        }

        public static void Remove(
            this RectOffset rectOffset,
            RectOffset otherRectOffset)
        {
            rectOffset.top -= otherRectOffset.top;
            rectOffset.bottom -= otherRectOffset.bottom;
            rectOffset.left -= otherRectOffset.left;
            rectOffset.right -= otherRectOffset.right;
        }
    }
}
