using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace AAA.Utility.EventCallers
{
    public class RepeatEvent : MonoBehaviour
    {
        [SerializeField] private float delayTime;
        [SerializeField] private int repeatAmount = 1;
        [SerializeField] private bool repeatAtStart;
        [SerializeField] private UnityEvent onRepeated;

        void Start()
        {
            if (repeatAtStart)
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
            int repeatIndex = 0;
            YieldInstruction waitForSeconds = new WaitForSeconds(time);
            while (true)
            {
                yield return waitForSeconds;
                repeatIndex++;
                onRepeated?.Invoke();
                if( repeatAmount != 0 && repeatIndex >= repeatAmount)
                    break;
            }
        }
    }
}