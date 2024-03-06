using UnityEngine;
using UnityEngine.SceneManagement;

namespace AAA.Extensions
{
    public static class SceneExtensions
    {
        public static bool TryGetRootComponent<T>(this Scene scene, out T component)
        {
            var rootGameObjects = scene.GetRootGameObjects();

            foreach (var rootGameObject in rootGameObjects)
            {
                var hasComponent = rootGameObject.TryGetComponent(out component);

                if (!hasComponent)
                {
                    continue;
                }

                return true;
            }

            component = default;
            return false;
        }


        // SceneManager.GetSceneByBuildIndex(int buildIndex).Name or .Path returns null if the scene is not loaded
        // Bullshit, I know, but apparently NOT a bug: https://issuetracker.unity3d.com/issues/scenemanager-dot-getscenebybuildindex-dot-name-returns-an-empty-string-if-scene-is-not-loaded
        // GetScenePathByBuildIndex DOES work properly though.
        public static string SceneIndexToName(int buildIndex)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }


        // This is a WAY better name for the function
        public static bool SceneExists(string sceneName)
            => Application.CanStreamedLevelBeLoaded(sceneName);
    }
}