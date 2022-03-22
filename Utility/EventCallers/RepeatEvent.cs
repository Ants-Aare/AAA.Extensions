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
        public void StartRepeating()
        {
            StartRepeating(delayTime);
        }
        public void StartRepeating(float time)
        {
            StopAllCoroutines();
            StartCoroutine(Repeat(time));
        }
        public void StopRepeating()
        {
            StopAllCoroutines();
        }
        private IEnumerator Repeat(float time)
        {
            int repeatIndex = 0;
            YieldInstruction waitForSeconds = new WaitForSeconds(time);
            while (true)
            {
                yield return waitForSeconds;
                onRepeated?.Invoke();
                repeatIndex++;
                if( repeatAmount != 0 && repeatIndex >= repeatAmount)
                    break;
            }
        }
    }
}