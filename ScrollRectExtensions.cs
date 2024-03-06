using UnityEngine;
using UnityEngine.UI;

namespace AAA.Extensions
{
    public static class ScrollRectExtensions
    {
        public static void ScrollToEnd(this Scrollbar scrollbar)
            => scrollbar.value = 1;

        public static void ScrollToStart(this Scrollbar scrollbar)
            => scrollbar.value = 0;
        public static void ScrollToBottom(this ScrollRect scrollRect)
            => scrollRect.verticalNormalizedPosition = 0;

        public static void ScrollToTop(this ScrollRect scrollRect)
            => scrollRect.verticalNormalizedPosition = 1;

        public static float GetElementNormalizedHorizontalPosition(this ScrollRect scrollRect, RectTransform rectTransform)
        {
            var scrollWidth = scrollRect.content.rect.width;
            var localContentPosition = rectTransform.GetAnchoredPositionRelativeToRectTransform(scrollRect.content);

            return MathExtensions.GetNormalizedFactor(localContentPosition.x, scrollWidth);
        }
    }
}
