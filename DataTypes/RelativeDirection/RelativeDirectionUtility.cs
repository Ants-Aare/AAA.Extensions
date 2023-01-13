using System.Numerics;
using UnityEngine;

namespace AAA.DataTypes
{
    public static class RelativeDirectionUtility
    {
        public static Vector2Int DirectionToVector2Int(RelativeDirection direction)
        {
            switch (direction)
            {
                case RelativeDirection.Top:
                    return new Vector2Int(0, 1);
                case RelativeDirection.TopRight:
                    return new Vector2Int(1, 1);
                case RelativeDirection.Right:
                    return new Vector2Int(1, 0);
                case RelativeDirection.BottomRight:
                    return new Vector2Int(1, -1);
                case RelativeDirection.Bottom:
                    return new Vector2Int(0, -1);
                case RelativeDirection.BottomLeft:
                    return new Vector2Int(-1, -1);
                case RelativeDirection.Left:
                    return new Vector2Int(-1, 0);
                case RelativeDirection.TopLeft:
                    return new Vector2Int(-1, 1);
                default:
                    return new Vector2Int(0, 0);
            }
            
        }

        public static RelativeDirection Vector2IntToDirection(Vector2Int vector2Int)
        {
            if(vector2Int.y > 0)
            {
                if(vector2Int.x > 0) return RelativeDirection.TopRight;
                if(vector2Int.x < 0) return RelativeDirection.TopLeft;
                return RelativeDirection.Top;
            }
            if(vector2Int.y < 0)
            {
                if(vector2Int.x > 0) return RelativeDirection.BottomRight;
                if(vector2Int.x < 0) return RelativeDirection.BottomLeft;
                return RelativeDirection.Bottom;
            }
            
            if(vector2Int.x > 0) return RelativeDirection.Right;
            if(vector2Int.x < 0) return RelativeDirection.Left;
            return RelativeDirection.Center;
        }
    }
}