using UnityEngine;
using UnityEngine.UI;

namespace AAA.Utility.Extensions
{
    public static class ScrollRectExtensions
    {
        public static void ScrollUp(this ScrollRect scrollRect)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }

        public static float GetElementNormalizedHorizontalPosition(this ScrollRect scrollRect, RectTransform rectTransform)
        {
            float scrollWidth = scrollRect.content.rect.width;
            Vector2 localContentPosition = rectTransform.GetAnchoredPositionRelativeToRectTransform(scrollRect.content);

            return MathExtensions.GetNormalizedFactor(localContentPosition.x, scrollWidth);
        }
    }
}
