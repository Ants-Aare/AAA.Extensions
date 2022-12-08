using UnityEngine;

namespace AAA.Utility.Extensions
{
    public static class RectTransformExtensions
    {
        public static void SetAnchoredPositionX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(x, currPosition.y);
        }

        public static void SetAnchoredPositionY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x, y);
        }

        public static void AddAnchoredPositionX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x + x, currPosition.y);
        }

        public static void AddAnchoredPositionY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.anchoredPosition;
            transform.anchoredPosition = new Vector2(currPosition.x, currPosition.y + y);
        }

        public static void SetPivotX(this RectTransform transform, float x)
        {
            Vector2 currPosition = transform.pivot;
            transform.pivot = new Vector3(x, currPosition.y);
        }

        public static void SetPivotY(this RectTransform transform, float y)
        {
            Vector2 currPosition = transform.pivot;
            transform.pivot = new Vector3(currPosition.x, y);
        }

        public static void SetSizeDeltaX(this RectTransform transform, float x)
        {
            Vector2 currSizeDelta = transform.sizeDelta;
            transform.sizeDelta = new Vector2(x, currSizeDelta.y);
        }

        public static void SetSizeDeltaY(this RectTransform transform, float y)
        {
            Vector2 currSizeDelta = transform.sizeDelta;
            transform.sizeDelta = new Vector2(currSizeDelta.x, y);
        }

        public static void SetAnchorMaxX(this RectTransform transform, float x)
        {
            Vector2 currAnchor = transform.anchorMax;
            transform.anchorMax = new Vector3(x, currAnchor.x);
        }

        public static void SetAnchorMaxY(this RectTransform transform, float y)
        {
            Vector2 currAnchor = transform.anchorMax;
            transform.anchorMax = new Vector3(currAnchor.x, y);
        }

        public static Rect GetAnchoredRect(this RectTransform transform)
        {
            Vector2 halfSize = transform.sizeDelta * 0.5f;
            Vector2 anchoredPosition = transform.anchoredPosition;

            Rect bounds = new Rect
            {
                min = anchoredPosition - halfSize,
                max = anchoredPosition + halfSize,
            };

            return bounds;
        }

        public static Vector2 GetScreenPosition(this RectTransform transform)
        {
            return RectTransformUtility.WorldToScreenPoint(null, transform.position);
        }

        public static Rect GetScreenRect(this RectTransform transform)
        {
            Bounds bounds = transform.GetWorldCornersBounds();

            return RectExtensions.FromCenterAndSize(bounds.center.ToVector2XY(), bounds.size.ToVector2XY());
        }

        public static Vector2 GetAnchoredPositionFromScreenPosition(this RectTransform transform, Vector2 screenPosition)
        {
            RectTransform parentRectTransform = transform.parent as RectTransform;

            if (parentRectTransform == null)
            {
                return screenPosition;
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRectTransform,
                screenPosition,
                null,
                out Vector2 localPoint
            );

            return localPoint;
        }

        public static Vector2 GetScreenPositionFromAnchoredPosition(this RectTransform transform, Vector2 anchoredPosition)
        {
            RectTransform parentRectTransform = transform.parent as RectTransform;

            if (parentRectTransform == null)
            {
                return anchoredPosition;
            }

            Vector2 worldPosition = parentRectTransform.TransformPoint(anchoredPosition);

            return RectTransformUtility.WorldToScreenPoint(null, worldPosition);
        }

        public static Vector2 GetAnchoredPositionRelativeToRectTransform(this RectTransform transform, RectTransform to)
        {
            Vector2 transformPivot = transform.pivot;
            Rect transformRect = transform.rect;

            Vector2 toPivot = to.pivot;
            Rect toRect = to.rect;

            Vector2 transformPivotDerivedOffset = new Vector2(
                transformRect.width * transformPivot.x + transformRect.xMin,
                transformRect.height * transformPivot.y + transformRect.yMin
                );

            Vector2 toPivotDerivedOffset = new Vector2(
                toRect.width * toPivot.x + toRect.xMin,
                toRect.height * toPivot.y + toRect.yMin
                );

            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(null, transform.position);
            screenPosition += transformPivotDerivedOffset;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenPosition, null, out Vector2 localPoint);

            return to.anchoredPosition + localPoint - toPivotDerivedOffset;
        }

        public static void CopyValues(RectTransform from, RectTransform to)
        {
            to.anchorMin = from.anchorMin;
            to.anchorMax = from.anchorMax;
            to.anchoredPosition = from.anchoredPosition;
            to.sizeDelta = from.sizeDelta;
        }

        public static void SetLeft(this RectTransform transform, float left)
        {
            transform.offsetMin = new Vector2(left, transform.offsetMin.y);
        }

        public static float GetLeft(this RectTransform rt)
        {
            return rt.offsetMin.x;
        }

        public static float AddLeft(this RectTransform transform, float value)
        {
            var left = GetLeft(transform);
            var res = left + value;
            SetLeft(transform, res);
            return res;
        }

        public static float RemoveLeft(this RectTransform transform, float value)
        {
            var left = GetLeft(transform);
            var res = left - value;
            SetLeft(transform, res);
            return res;
        }

        public static void SetRight(this RectTransform transform, float right)
        {
            transform.offsetMax = new Vector2(-right, transform.offsetMax.y);
        }

        public static float GetRight(this RectTransform transform)
        {
            return -transform.offsetMax.x;
        }

        public static float AddRight(this RectTransform transform, float value)
        {
            var right = GetRight(transform);
            var res = right + value;
            SetRight(transform, res);
            return res;
        }

        public static float RemoveRight(this RectTransform transform, float value)
        {
            var right = GetRight(transform);
            var res = right - value;
            SetRight(transform, res);
            return res;
        }

        public static void SetTop(this RectTransform transform, float top)
        {
            transform.offsetMax = new Vector2(transform.offsetMax.x, -top);
        }

        public static float GetTop(this RectTransform transform)
        {
            return -transform.offsetMax.y;
        }

        public static float AddTop(this RectTransform transform, float value)
        {
            var top = GetTop(transform);
            var res = top + value;
            SetTop(transform, res);
            return res;
        }

        public static float RemoveTop(this RectTransform transform, float value)
        {
            var top = GetTop(transform);
            var res = top - value;
            SetTop(transform, res);
            return res;
        }

        public static void SetBottom(this RectTransform transform, float bottom)
        {
            transform.offsetMin = new Vector2(transform.offsetMin.x, bottom);
        }

        public static float GetBottom(this RectTransform transform)
        {
            return transform.offsetMin.y;
        }

        public static float AddBottom(this RectTransform transform, float value)
        {
            var bottom = GetBottom(transform);
            var res = bottom + value;
            SetBottom(transform, res);
            return res;
        }

        public static float RemoveBottom(this RectTransform transform, float value)
        {
            var bottom = GetBottom(transform);
            var res = bottom - value;
            SetBottom(transform, res);
            return res;
        }

        public static void AddRectOffsets(
            this RectTransform transform,
            RectOffset rectOffset)
        {
            transform.AddBottom(rectOffset.bottom);
            transform.AddTop(rectOffset.top);
            transform.AddLeft(rectOffset.left);
            transform.AddRight(rectOffset.right);
        }

        public static void RemoveRectOffsets(
            this RectTransform transform,
            RectOffset rectOffset)
        {
            transform.RemoveBottom(rectOffset.bottom);
            transform.RemoveTop(rectOffset.top);
            transform.RemoveLeft(rectOffset.left);
            transform.RemoveRight(rectOffset.right);
        }

        static readonly Vector3[] WorldCorners = new Vector3[4];
        public static Bounds GetWorldCornersBounds(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(WorldCorners);
            Bounds bounds = new Bounds(WorldCorners[0], Vector3.zero);
            for(int i = 1; i < 4; ++i)
            {
                bounds.Encapsulate(WorldCorners[i]);
            }
            return bounds;
        }

        public static float GetWorldCornersWidth(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(WorldCorners);
            return Vector3.Distance(WorldCorners[0], WorldCorners[3]);
        }

        public static float GetWorldCornersHeight(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(WorldCorners);
            return Vector3.Distance(WorldCorners[0], WorldCorners[1]);
        }

        public static Vector2 GetWorldCornersSize(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(WorldCorners);
            return new Vector2(
                Vector3.Distance(WorldCorners[0], WorldCorners[3]),
                Vector3.Distance(WorldCorners[0], WorldCorners[1])
            );
        }

        public static bool ContainsScreenPosition(this RectTransform rectTransform, Vector2 screenPosition)
        {
            Vector2 positionInLocalSpace = rectTransform.InverseTransformPoint(screenPosition);
            return rectTransform.rect.Contains(positionInLocalSpace);
        }
    }
}
