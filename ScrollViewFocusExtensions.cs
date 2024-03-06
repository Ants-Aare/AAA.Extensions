using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AAA.Extensions
{
    public static class ScrollViewFocusExtensions
    {
        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            var contentSize = scrollView.content.rect.size;
            var viewportSize = ((RectTransform)scrollView.content.parent).rect.size;
            Vector2 contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            var scrollPosition = scrollView.normalizedPosition;
            // if (scrollView.horizontal && contentSize.x > viewportSize.x)
            //     scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            // if (scrollView.vertical && contentSize.y > viewportSize.y)
            //     scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            if (scrollView.horizontal)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));

            var contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);

            var focusedScrollPointPosition=  scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
            return focusedScrollPointPosition;
        }

        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint);
        }

        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item)
        {
            scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item);
        }

        static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            var initialNormalizedPos = scrollView.normalizedPosition;

            var t = 0f;
            while (t < 1f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(initialNormalizedPos, targetNormalizedPos, 1f - (1f - t) * (1f - t));

                yield return null;
                t += speed * UnityEngine.Time.unscaledDeltaTime;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
        }

        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed);
        }

        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed)
        {
            yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed);
        }
    }
}
