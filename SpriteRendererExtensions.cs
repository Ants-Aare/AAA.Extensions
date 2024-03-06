using UnityEngine;

namespace AAA.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float normalizedAlpha)
        {
            var color = spriteRenderer.color;
            color.a = normalizedAlpha;
            spriteRenderer.color = color;
        }
    }
}
