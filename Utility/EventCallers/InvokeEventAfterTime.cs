using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace AAA.Utility.EventCallers
{
    public class InvokeEventAfterTime : MonoBehaviour
    {
        [SerializeField]
        private bool invokeAtStart = true;
        [SerializeField]
        private float timeDelay;
        [SerializeField]
        private UnityEvent unityEvent = null;

        private void Start()
        {
            if (invokeAtStart)
                StartCoroutine(InvokeAfterTime(timeDelay));
        }

        public void InvokeEvent()
        {
            StartCoroutine(InvokeAfterTime(timeDelay));
        }
        public void InvokeEvent(float time)
        {
            StartCoroutine(InvokeAfterTime(time));
        }

        private IEnumerator InvokeAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            unityEvent.Invoke();
        }
    }
}