namespace AAA.Utility.Extensions
{
    public static class UnityObjectExtensions
    {
        public static void Destroy(this UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(obj);
        }

        public static void DestroyImmediate(this UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return;
            }

            UnityEngine.Object.DestroyImmediate(obj);
        }
    }
}
