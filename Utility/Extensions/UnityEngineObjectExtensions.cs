namespace AAA.Utility.Extensions
{
    public static class UnityEngineObjectExtensions
    {
        /// <summary>
        /// Unity has a cool feature where they serialize fake Null UnityEngine.Objects instead of actual null objects
        /// This means that you can't use the '?' or '??' operators with them
        /// After applying this extension, it is safe to use them
        /// </summary>
        /// <example>gameObject.Null()?.SetActive(true)</example>
        public static T Null<T>(
            this T value)
            where T : UnityEngine.Object
        {
            if (!value)
            {
                return null;
            }

            return value;
        }
    }
}
