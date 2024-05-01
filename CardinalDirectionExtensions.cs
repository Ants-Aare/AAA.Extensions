using System;
using System.Linq;
using UnityEngine;

namespace AAA.Extensions
{
    public enum CardinalDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class CardinalDirectionExtensions
    {
        public static readonly float DotProduct0Degrees = 1f;
        public static readonly float DotProduct45Degrees = 0.707f;
        public static readonly float DotProduct90Degrees = 0f;

        public static OrdinalDirection ToOrdinalDirection(this CardinalDirection value)
            => (OrdinalDirection)((int)value * 2);

        public static CardinalDirection ToCardinalDirection(this Vector2 value)
        {
            value = value.normalized;

            var dotProductUp = Vector2.Dot(Vector2.up, value);
            var dotProductRight = Vector2.Dot(Vector2.right, value);

            if (dotProductUp >= DotProduct45Degrees) return CardinalDirection.Up;
            if (dotProductUp <= -DotProduct45Degrees) return CardinalDirection.Down;

            if (dotProductRight >= DotProduct45Degrees) return CardinalDirection.Right;
            if (dotProductRight <= -DotProduct45Degrees) return CardinalDirection.Left;

            return CardinalDirection.Up;
        }
        public static bool IsAlongAxis(this CardinalDirection direction, Vector2 value)
        {
            var cardinalDirection = value.ToCardinalDirection();

            return direction switch
            {
                CardinalDirection.Up => cardinalDirection is CardinalDirection.Up or CardinalDirection.Down,
                CardinalDirection.Right =>  cardinalDirection is CardinalDirection.Right or CardinalDirection.Left,
                CardinalDirection.Down =>  cardinalDirection is CardinalDirection.Up or CardinalDirection.Down,
                CardinalDirection.Left =>  cardinalDirection is CardinalDirection.Right or CardinalDirection.Left,
                _ => false,
            };
        }

        public static CardinalDirection ToCardinalDirection(this Vector2Int value)
        {
            if (value == Vector2Int.up) return CardinalDirection.Up;
            if (value == Vector2Int.down) return CardinalDirection.Down;

            if (value == Vector2Int.right) return CardinalDirection.Right;
            if (value == Vector2Int.left) return CardinalDirection.Left;

            return CardinalDirection.Up;
        }

        public static Quaternion ToQuaternion(this CardinalDirection direction)
            => Quaternion.Euler(direction.ToEuler());

        public static Quaternion ToQuaternionZ(this CardinalDirection direction)
            => Quaternion.Euler(direction.ToEulerZ());

        public static Vector3 ToEuler(this CardinalDirection direction)
            => new(0, (int)direction * 90f, 0);

        public static Vector3 ToEulerZ(this CardinalDirection direction)
            => new(0, 0, (int)direction * 90f);

        public static CardinalDirection Rotate(this CardinalDirection direction, int iterations)
        {
            var index = (int)direction;
            index = (index + iterations) % 4;
            return (CardinalDirection)index;
        }

        public static Vector3Int Rotate(this Vector3Int value, CardinalDirection direction)
            => direction switch
            {
                CardinalDirection.Up => value,
                CardinalDirection.Right => new Vector3Int(value.z, value.y, -value.x),
                CardinalDirection.Down => new Vector3Int(-value.x, value.y, -value.z),
                CardinalDirection.Left => new Vector3Int(-value.z, value.y, value.x),
                _ => value
            };

        public static Vector3 Rotate(this Vector3 value, CardinalDirection direction)
            => direction switch
            {
                CardinalDirection.Up => value,
                CardinalDirection.Right => new Vector3(value.z, value.y, -value.x),
                CardinalDirection.Down => new Vector3(-value.x, value.y, -value.z),
                CardinalDirection.Left => new Vector3(-value.z, value.y, value.x),
                _ => value
            };

        public static CardinalDirection Flip(this CardinalDirection direction) => direction.Rotate(2);

        public static CardinalDirection FlipX(this CardinalDirection direction)
            => direction is CardinalDirection.Left or CardinalDirection.Right
                ? direction.Rotate(2)
                : direction;

        public static CardinalDirection FlipY(this CardinalDirection direction)
            => direction is CardinalDirection.Left or CardinalDirection.Right
                ? direction.Rotate(2)
                : direction;

        public static Vector2Int ToVector2Int(this CardinalDirection value)
            => value switch
            {
                CardinalDirection.Up => Vector2Int.up,
                CardinalDirection.Right => Vector2Int.right,
                CardinalDirection.Down => Vector2Int.down,
                CardinalDirection.Left => Vector2Int.left,
                _ => Vector2Int.up
            };

        static Vector2Int[] _directionVectors;

        public static Vector2Int[] DirectionVectors
            => _directionVectors ??= ((OrdinalDirection[])Enum.GetValues(typeof(OrdinalDirection)))
                .Select(direction => direction.ToVector2Int())
                .ToArray();
    }
}