using System;
using NaughtyAttributes;
using UnityEngine;

namespace AAA.Utility.GameObjectUtil
{
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private bool useMainCamera;

        [HideIf("useMainCamera")]
        [SerializeField] private Transform targetTransform;

        private void Start()
        {
            targetTransform = Camera.main.transform;
        }
        private void LateUpdate()
        {
            transform.LookAt(targetTransform);
        }
    }
}