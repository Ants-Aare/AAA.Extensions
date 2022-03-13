using UnityEngine;
using UnityEngine.Events;
using System.Collections;
public class RepeatEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onRepeat;
    [SerializeField] private float delayTime;
    [SerializeField] private bool repeatFromStart;

    void Start()
    {
        if(repeatFromStart)
            StartRepeating(delayTime);
    }
    private void StartRepeating()
    {
        StartRepeating(delayTime);
    }
    private void StartRepeating(float time)
    {
        StopAllCoroutines();
        StartCoroutine(Repeat(time));
    }
    private IEnumerator Repeat(float time)
    {
        while(true)
        {
            onRepeat?.Invoke();
            yield return new WaitForSeconds(time);
        }
    }
}