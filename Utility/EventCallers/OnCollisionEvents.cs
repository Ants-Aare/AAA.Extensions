using UnityEngine;
using AAA.Utility.CustomUnityEvents;

namespace AAA.Utility.EventCallers
{
    public class OnCollisionEvents : MonoBehaviour
    {
        [SerializeField] private CollisionUnityEvent onCollisionEntered = null;
        [SerializeField] private CollisionUnityEvent onCollisionExited = null;

        private void OnCollisionEnter(Collision collision)=> onCollisionEntered?.Invoke(collision);
        private void OnCollisionExit(Collision collision)=> onCollisionExited?.Invoke(collision);
    }
}