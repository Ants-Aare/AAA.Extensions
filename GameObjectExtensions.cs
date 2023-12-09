using System.Collections.Generic;
using UnityEngine;

namespace AAA.Extensions
{
    public static class GameObjectExtensions
    {
        public static void SetActive(IReadOnlyList<GameObject> gameObjects, bool active)
        {
            foreach (GameObject gameObject in gameObjects)
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
            bool found = gameObject.TryGetComponent(out T component);

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

        public static void SetInteractable(this GameObject gameObject, bool interactable)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.interactable = interactable;
        }

        public static bool IsInteractable(this GameObject gameObject)
        {
            bool found = gameObject.TryGetComponent(out CanvasGroup canvasGroup);

            if (!found)
            {
                return true;
            }

            return canvasGroup.interactable;
        }

        public static void SetAlpha(this GameObject gameObject, float alpha)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.alpha = alpha;
        }

        public static void SetIntaractableAndBlocksRaycasts(this GameObject gameObject, bool interactableAndBlocksRaycasts)
        {
            CanvasGroup canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

            canvasGroup.interactable = interactableAndBlocksRaycasts;
            canvasGroup.blocksRaycasts = interactableAndBlocksRaycasts;
        }
    }
}
