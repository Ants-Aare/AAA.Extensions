using UnityEngine;
using UnityEngine.Events;

namespace AAA.GlobalVariables.Variables
{
    public class GlobalEventListener : MonoBehaviour, IEventListener
    {
        [SerializeField] private GlobalEvent globalEvent;
        [SerializeField] private UnityEvent onEventRaised;

        void OnEnable()
        {
            globalEvent.AddListener(this); ;
        }
        void OnDisable()
        {
            globalEvent.RemoveListener(this); ;
        }

        public void OnEventRaised()
        {
            onEventRaised?.Invoke();
        }
    }
}