using System;
using System.Linq;
using UnityEngine;

namespace AAA.Extensions
{
    public enum OrdinalDirection
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7
    }

    public static class OrdinalDirectionExtensions
    {
        public static CardinalDirection ToCardinalDirection(this OrdinalDirection value)
            => (CardinalDirection)((int)value / 2);

        public static Vector2Int ToVector2Int(this OrdinalDirection value)
        {
            var index = (int)value;
            var cardinalDirection = value.ToCardinalDirection().ToVector2Int();
            if (index % 2 == 0)
            {
                return cardinalDirection;
            }

            // Rotates the direction by 90 degrees and adds it to the normal result
            var offsettedIndex = index / 2 + 1 % 4;
            var offsettedDirection = (CardinalDirection)offsettedIndex;
            return cardinalDirection + offsettedDirection.ToVector2Int();
        }

        static Vector2Int[] _directionVectors;

        public static Vector2Int[] DirectionVectors
            => _directionVectors ??= ((OrdinalDirection[])Enum.GetValues(typeof(OrdinalDirection)))
                .Select(direction => direction.ToVector2Int())
                .ToArray();
    }
}