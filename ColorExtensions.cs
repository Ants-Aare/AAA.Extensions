using UnityEngine;

namespace AAA.Extensions
{
    public static class ColorExtensions
    {
        public static bool IsHtmlColor(this Color color, string htmlColor)
        {
            var pixelHtmlColor = ColorUtility.ToHtmlStringRGB(color);

            string FormatHtmlColor(string htmlColorString)
            {
                if (string.IsNullOrEmpty(htmlColorString))
                {
                    return htmlColorString;
                }

                return htmlColorString.Replace("#", "").ToLower();
            }

            return string.Equals(FormatHtmlColor(pixelHtmlColor), FormatHtmlColor(htmlColor));
        }

        public static Color FromHtmlColor(string htmlColor)
        {
            var couldParse = ColorUtility.TryParseHtmlString(htmlColor, out var color);

            if (!couldParse)
            {
                return Color.magenta;
            }

            return color;
        }

        public static Color FromRGBA255(float r, float g, float b, float a)
        {
            var delta = 1f / 255f;

            return new Color(delta * r, delta * g, delta * b, delta * a);
        }

        public static Color FromRGB255(float r, float g, float b)
        {
            var delta = 1f / 255f;

            return new Color(delta * r, delta * g, delta * b, 1);
        }
    }
}
