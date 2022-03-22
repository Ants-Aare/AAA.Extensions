using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace AAA.Utility.EventCallers
{
    public class MaximumRepeatsEvent : MonoBehaviour
    {
        [SerializeField] private int maximumRepeats;
        [ShowInInspector, ReadOnly] private int currentRepeats = 0;
        [SerializeField] private UnityEvent onEventCalled;

        public void OnRepeat()
        {
            currentRepeats++;
            if (currentRepeats <= maximumRepeats)
                onEventCalled?.Invoke();
        }
        
        public void ResetRepeats()
        {
            currentRepeats = 0;
        }

        public void SetMaximumRepeats(int value)
        {
            maximumRepeats = value;
        }
    }
}