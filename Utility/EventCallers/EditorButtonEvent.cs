
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace AAA.Utility.EventCallers
{
    public class EditorButtonEvent : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onButtonClickedEvent = null;

        [Button]
        public void InvokeEvent()
        {
            onButtonClickedEvent.Invoke();
        }
    }
}   