using UnityEngine;
using UnityEngine.Events;

public class MinimumRepeatsEvent : MonoBehaviour
{
    [SerializeField] private int minimumRepeats;
    [SerializeField] private int currentRepeats;
    [SerializeField] private UnityEvent onFinished;

    public void OnRepeat()
    {
        currentRepeats++;
        if(currentRepeats == minimumRepeats)
            onFinished?.Invoke();
    }
}