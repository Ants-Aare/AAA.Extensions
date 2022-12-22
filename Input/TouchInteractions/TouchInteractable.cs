using System;

using UnityEngine;

namespace AAA.Mobile.Input.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class TouchInteractable : MonoBehaviour, iTouchInteractable
    {
        [SerializeField] protected bool enableOnStart = false;
        [SerializeField, HideInInspector] private Collider col;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if(col == null)
                col = GetComponent<Collider>();
        }
        #endif

        protected virtual void Start()
        {
            if (enableOnStart)
                EnableInteractability();
            else
                DisableInteractability();
        }

        public virtual void EnableInteractability()
        {
            col.enabled = true;
            // Debug.Log($"Enabling Interactability for GameObject {gameObject.name}", gameObject);
        }

        public virtual void DisableInteractability()
        {
            col.enabled = false;
            // Debug.Log($"Disabling Interactability for GameObject {gameObject.name}", gameObject);
        }

        public virtual void StartTouchInteraction(TouchInputAction touchInputAction)
        {
        }

        public virtual void EndTouchInteraction(TouchInputAction touchInputAction)
        {
        }
    }
}
