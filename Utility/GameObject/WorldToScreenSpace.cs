using System;
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class WorldToScreenSpace : MonoBehaviour
    {
        [SerializeField] private Vector3 offset;
        [SerializeField] private Transform sourceWorldSpaceTransform;
        [SerializeField] private Transform targetScreenSpaceTransform;
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
