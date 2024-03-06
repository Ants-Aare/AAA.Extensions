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
            var parentTransform = (RectTransform)gridLayout.transform.parent;
            var parentWidth = parentTransform.rect.width;

            var gridLayoutConstraintCount = gridLayout.constraintCount;

            var spacingSize = gridLayout.padding.left +
                              gridLayout.padding.right +
                              gridLayout.spacing.x * (gridLayoutConstraintCount - 1);

            var remainingSize = parentWidth - spacingSize;

            var cellWidth = MathExtensions.Divide(remainingSize, gridLayoutConstraintCount);

            return cellWidth;
        }
    }
}
