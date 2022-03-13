using UnityEngine;

namespace AAA.Utility.GlobalVariables
{
    public class RaiseEventOnTrigger : MonoBehaviour
    {
        [SerializeField]
        private GlobalEvent enterEvent;
        [SerializeField]
        private GlobalEvent exitEvent;

        void OnTriggerEnter(Collider other)
        {
            enterEvent?.Raise();
        }
        void OnTriggerExit(Collider other)
        {
            exitEvent?.Raise();
        }
    }
}