using System;
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class WorldToScreenSpace : MonoBehaviour
    {
        public Transform sourceWorldSpaceTransform;
        public Transform targetScreenSpaceTransform;
        public Vector3 offset;
        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        public void Update()
        {
            Vector3 position = mainCamera.WorldToScreenPoint(sourceWorldSpaceTransform.position);

            targetScreenSpaceTransform.position = position + offset;
        }
    }
}
