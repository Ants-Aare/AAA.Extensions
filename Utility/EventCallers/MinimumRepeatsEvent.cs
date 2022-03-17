using UnityEngine;
using UnityEngine.Events;

namespace AAA.Utility.EventCallers
{
    public class MinimumRepeatsEvent : MonoBehaviour
    {
        [SerializeField] private int minimumRepeats;
        [SerializeField] private int currentRepeats = 0;
        [SerializeField] private UnityEvent onFinished;

        public void OnRepeat()
        {
            currentRepeats++;
            if (currentRepeats == minimumRepeats)
                onFinished?.Invoke();
        }
        
        public void ResetRepeats()
        {
            currentRepeats = 0;
        }

        public void SetMinimumRepeats(int value)
        {
            minimumRepeats = value;
        }
    }
}