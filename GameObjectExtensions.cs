using System.Collections.Generic;
using UnityEngine;

namespace AAA.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetActive(IReadOnlyList<GameObject> gameObjects, bool active)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(active);
            }
        }

        public static void RemoveParentAndDestroy(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.RemoveParent();

            Object.Destroy(gameObject);
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var found = gameObject.TryGetComponent(out T component);

            return found ? component : gameObject.AddComponent<T>();
        }

        public static void SetParent(this GameObject gameObject, GameObject parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent == null ? null : parent.transform, worldPositionStays);
        }

        public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        public static void RemoveParent(this GameObject gameObject, bool worldPositionStays = true)
        {
            gameObject.transform.SetParent(null, worldPositionStays);
        }
        
        public static void SetLayerRecursively(this GameObject go, int layer)
        {
            go.layer = layer;

            for (int i = 0; i < go.transform.childCount; i++)
                go.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
        }

        public static RectTransform GetRectTransform(this GameObject go)
        {
            return (RectTransform)go.transform;
        }
    }
}
