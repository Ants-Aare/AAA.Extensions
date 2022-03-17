using UnityEngine;
using System.Collections.Generic;

namespace AAA.Utility.GameObjectUtil
{
    public class DestroyGameObject : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private bool destroyOnStart = false;
        [SerializeField] private float delay = 0f;

        private void Start()
        {
            if (destroyOnStart)
            {
                if (delay > 0)
                    Destroy(gameObject, delay);
                else
                    Destroy(gameObject);
            }
        }

        public void DestroyThisGameObject()
        {
            Destroy(gameObject);
        }
    }
}