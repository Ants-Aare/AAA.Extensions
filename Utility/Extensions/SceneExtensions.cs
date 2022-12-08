using UnityEngine;
using UnityEngine.SceneManagement;

namespace AAA.Utility.Extensions
{
    public static class SceneExtensions
    {
        public static bool TryGetRootComponent<T>(this Scene scene, out T component)
        {
            GameObject[] rootGameObjects = scene.GetRootGameObjects();

            foreach (GameObject rootGameObject in rootGameObjects)
            {
                bool hasComponent = rootGameObject.TryGetComponent(out component);

                if (!hasComponent)
                {
                    continue;
                }

                return true;
            }

            component = default;
            return false;
        }
    }
}
