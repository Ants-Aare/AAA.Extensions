using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace AAA.Utility.GameObjectUtil
{
    public class SetObjectLayer : MonoBehaviour
    {
        [SerializeField] int targetLayer;
        public void SetLayer(GameObject obj)
        {
            obj.layer = targetLayer;
        }

        public void SetLayerRecursively(Transform targetTransform)
        {
            targetTransform.gameObject.layer = targetLayer;
            foreach (Transform child in targetTransform)
            {
                SetLayerRecursively(child);
            }
        }
    }
}