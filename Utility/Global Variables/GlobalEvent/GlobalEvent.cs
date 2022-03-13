using UnityEngine;
using System.Collections.Generic;

namespace AAA.Utility.GlobalVariables
{
    [CreateAssetMenu(menuName = "Variable/Global Event")]
    public class GlobalEvent : ScriptableObject
    {
        /*[ShowInInspector]*/
        private List<IEventListener> eventListeners = new List<IEventListener>();
        public virtual void Raise()
        {
            foreach (IEventListener eventListener in eventListeners)
            {
                eventListener.OnEventRaised();
            }
        }

        public void AddListener(IEventListener eventListener)
        {
            eventListeners.Add(eventListener);
        }
        public void RemoveListener(IEventListener eventListener)
        {
            eventListeners.Remove(eventListener);
        }
    }
}