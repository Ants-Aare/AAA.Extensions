using UnityEngine;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Utility.EventCallers
{
    public class OnTriggerEvents : MonoBehaviour
    {
        [SerializeField] private GameObjectUnityEvent onTriggerEntered;
        [SerializeField] private GameObjectUnityEvent onTriggerExited;

        private void OnTriggerEnter(Collider collider) => onTriggerEntered.Invoke(collider.gameObject);
        private void OnTriggerExit(Collider collider) => onTriggerExited.Invoke(collider.gameObject);
    }
}