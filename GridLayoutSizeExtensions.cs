using AAA.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace AAA.Extensions
{
    public static class GridLayoutSizeExtensions
    {
        public static float CalculateGridLayoutCellWidthWithColumnConstraintToTakeAllAvailableHorizontalSpace(
            this GridLayoutGroup gridLayout
            )
        {
            RectTransform parentTransform = (RectTransform)gridLayout.transform.parent;
            float parentWidth = parentTransform.rect.width;

            int gridLayoutConstraintCount = gridLayout.constraintCount;

            float spacingSize = gridLayout.padding.left +
                gridLayout.padding.right +
                gridLayout.spacing.x * (gridLayoutConstraintCount - 1);

            float remainingSize = parentWidth - spacingSize;

            float cellWidth = MathExtensions.Divide(remainingSize, gridLayoutConstraintCount);

            return cellWidth;
        }
    }
}
