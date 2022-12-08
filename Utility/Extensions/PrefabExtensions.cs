#if UNITY_EDITOR
using UnityEditor.SceneManagement;

namespace AAA.Utility.Extensions
{
    public static class PrefabExtensions
    {
        public static bool IsPrefabInContext(string assetPath)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();

            if (prefabStage == null)
            {
                return false;
            }

            if (!string.Equals(prefabStage.assetPath, assetPath))
            {
                return false;
            }

            return prefabStage.mode == PrefabStage.Mode.InContext;
        }
    }
}
#endif
