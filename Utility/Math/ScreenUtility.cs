using UnityEngine;

namespace AAA.Utility.Math
{
    public static class ScreenUtility
    {
        public static Vector2 ScreenToViewPort(Vector2 vector2)
        {
            vector2.x = vector2.x / Screen.width;
            vector2.y = vector2.y / Screen.height;
            return vector2;
        }
        public static Vector2 ViewportToScreen(Vector2 vector2)
        {
            vector2.x = vector2.x * Screen.width;
            vector2.y = vector2.y * Screen.height;
            return vector2;
        }

        // This will make the distance of the vector not based on aspect ratio. The y value may go above 1.
        public static Vector2 FixViewportDistanceHeight(Vector2 vector2)
        {
            vector2.y = vector2.y * Screen.height;
            vector2.y = vector2.y / Screen.width;
            return vector2;
        }

        // This will make the distance of the vector not based on aspect ratio. The x value may go above 1.
        public static Vector2 FixViewportDistanceWidth(Vector2 vector2)
        {
            vector2.x = vector2.x * Screen.width;
            vector2.x = vector2.x / Screen.height;
            return vector2;
        }

    }
}